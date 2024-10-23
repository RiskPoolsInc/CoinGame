using System.IO.Compression;
using System.Text;
using System.Text.Json.Serialization;

using App.Caching.Modules;
using App.Common.Modules;
using App.Core.Commands.Handlers.Mapping;
using App.Core.Commands.Handlers.Modules;
using App.Core.Configurations;
using App.Core.Configurations.Factory;
using App.Core.Configurations.Modules;
using App.Core.Pipeline.Modules;
using App.Core.Pipeline.Validators.Modules;
using App.Core.Requests.Handlers.Modules;
using App.Data.Mapping;
using App.Data.Sql.Core.Modules;
using App.Data.Sql.MigrateAfterBuild;
using App.Data.Sql.Modules;
using App.Data.Storage.Modules;
using App.Repositories.Modules;
using App.Security.Modules;
using App.Services.BlockChainExplorer.Modules;
using App.Services.WalletService.Modules;
using App.Web.Core.Converters;
using App.Web.Core.Filters;
using App.Web.Core.ModelBinders;
using App.Web.Core.Options;

using Autofac;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Swashbuckle.AspNetCore.SwaggerGen;

using CommandToEntityProfile = App.Data.Mapping.CommandToEntityProfile;

namespace TS.WebApi;

public class Startup {
    private readonly bool _isDevelop;
    private readonly string[] DefaultRoutes = { "/" };

    public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment) {
        Configuration = configuration;
#if DEBUG
        _isDevelop = true;
#endif
    }

    public IConfiguration Configuration { get; }

    public void ConfigureContainer(ContainerBuilder builder) {
        builder.RegisterModule<SimpleFactoryModule>();

        builder.RegisterModule(new AutoMapperModule(cfg => {
            cfg.AddProfile<CommandToEntityProfile>();
            cfg.AddProfile<CommandToEntityProfile>();

            cfg.AddProfile<EntityToViewProfile>();
            cfg.AddProfile<RequestToFilterProfile>();

            cfg.AddProfile<CommandToAdminProfile>();
            cfg.AddProfile<CommandToModelProfile>();
            cfg.AddProfile<CommandToCommandProfile>();

            cfg.AddProfile<ExternalViewProfile>();
        }));
        builder.RegisterModule<AppDbContextModule>();

        builder.RegisterModule<MigrationModule>();
        builder.RegisterModule<ConfigurationModule>();

        builder.RegisterModule<RepositoryModule>();

        builder.RegisterModule<StorageModule>();

        builder.RegisterModule<RequestHandlersModule>();
        builder.RegisterModule<CommandHandlersModule>();

        builder.RegisterModule<PreRequestHandlersModule>();
        builder.RegisterModule<ValidationModule>();
        builder.RegisterModule<DispatcherModule>();
        builder.RegisterModule<SecurityModule>();
        builder.RegisterModule<MigrationConfigModule>();
        builder.RegisterModule<MemoryCacheModule>();
        builder.RegisterModule<AppSettingsModule>();
        builder.RegisterModule<BlockChainExplorerModule>();
        builder.RegisterModule<WalletServiceModule>();

        builder.RegisterModule<MigrateAfterBuildModule<RunMigrateAfterBuild, string[]>>();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services) {
        // ConfigureCompression(services);
        // // Adds Microsoft Identity platform (AAD v2.0) support to protect this API
        // services.AddMicrosoftIdentityWebApiAuthentication(Configuration);

        //TODO Add DbContextOfApplication
        //services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseNpgsql(
        //        Configuration.GetConnectionString("DefaultConnection")));

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        var configurationFactory = new ConfigFactory(Configuration);


        services.AddApplicationInsightsTelemetry();

        services.AddCors(o => o.AddPolicy("CoreCorsPolicy", options => {
            options.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        }));

        services.AddVersionedApiExplorer(options => {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddApiVersioning(options => {
            options.UseApiBehavior = false;
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        services.AddControllers(options => {
                     options.RespectBrowserAcceptHeader = true;
                     options.Filters.Add(typeof(HandleErrorFilter));
                     options.Filters.Add(new ValidationModelFilter());
                     options.ModelBinderProviders.Insert(0, new ModelBinderProvider());
                 })
                .AddNewtonsoftJson(options => {
                     options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                     options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                     options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                     options.SerializerSettings.Converters.Insert(0, new TrimmingConverter());
                 })
                .AddJsonOptions(options => {
                     options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                     options.JsonSerializerOptions.PropertyNamingPolicy = null;
                 });

        services.AddSwaggerGen(options => {
            options.OperationFilter<CancellationTokenFilter>();
            options.OperationFilter<AddRequiredHeaderParameter>();
            var basePath = AppContext.BaseDirectory;
            var xmlPath = Path.Combine(basePath, @"TS.WebApi.xml");

            if (File.Exists(xmlPath))
                options.IncludeXmlComments(xmlPath);
        });

        services.AddControllersWithViews()
                .AddJsonOptions(options =>
                     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        ConfigureCompression(services);
    }

    public void ConfigureCompression(IServiceCollection services) {
        services.AddResponseCompression(options => { options.Providers.Add<GzipCompressionProvider>(); });

        services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostEnvironment env, IApiVersionDescriptionProvider provider) {
        app.Use(async (context, next) => {
            if (DefaultRoutes.Contains(context.Request.Path.Value.ToLowerInvariant())) {
                context.Response.Redirect("/api/index.html");
                return;
            }

            await next();
        });
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors("CoreCorsPolicy");
        app.UseResponseCompression();
        app.UseEndpoints(c => c.MapControllers());

        app.UseSwagger(c => {
            c.PreSerializeFilters.Add((swaggerDoc, httpReq) => {
                swaggerDoc.Servers = new List<OpenApiServer> {
                    new() {
                        Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/"
                    }
                };
            });
        });
        app.UseDeveloperExceptionPage();

        app.UseSwaggerUI(c => {
            foreach (var description in provider.ApiVersionDescriptions)
                c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

            c.RoutePrefix = "api";
            c.DocumentTitle = "TS API";
        });
    }
}