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
using App.Infrastructure.VirtualWalletService.Modules;
using App.Repositories.Modules;
using App.Security.Modules;
using App.Services.Base.Captcha;
using App.Services.BlockChainExplorer.Modules;
using App.Services.Telegram.Modules;
using App.Services.UbikiriApiService.Modules;
using App.Services.WalletService;
using App.Web.Core.Converters;
using App.Web.Core.Filters;
using App.Web.Core.ModelBinders;
using App.Web.Core.Options;

using Autofac;

using CF.WebApi.Configuration.Modules;

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
using ExternalViewToCommandProfile = App.Data.Mapping.ExternalViewToCommandProfile;



namespace CF.WebApi;

public class Startup
{
    private readonly bool _isDevelop;
    private readonly string[] DefaultRoutes = {"/"};

    public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        Configuration = configuration;
#if DEBUG
        _isDevelop = true;
#endif
    }

    public IConfiguration Configuration { get; }


    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule<SimpleFactoryModule>();

        builder.RegisterModule(new AutoMapperModule(cfg =>
        {
            cfg.AddProfile<CommandToEntityProfile>();
            cfg.AddProfile<CommandToEntityProfile>();
            
            cfg.AddProfile<EntityToViewProfile>();
            cfg.AddProfile<RequestToFilterProfile>();
            
            cfg.AddProfile<CommandToAdminProfile>();
            cfg.AddProfile<CommandToModelProfile>();
            
            cfg.AddProfile<ExternalViewToCommandProfile>();
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
        builder.RegisterModule<VirtualWallerServiceModule>();
        builder.RegisterModule<CaptchaServiceModule>();
        builder.RegisterModule<UbikiriApiModule>();
        builder.RegisterModule<TelegramModule>();
        builder.RegisterModule<ExternalSystemsApiKeysModule>();
        builder.RegisterModule<AppSettingsModule>();
        builder.RegisterModule<BlockChainExplorerModule>();
        
        builder.RegisterType<WalletService>().SingleInstance();
        builder.RegisterModule<MigrateAfterBuildModule<RunMigrateAfterBuild, string[]>>();
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
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

        services.AddCors(o => o.AddPolicy("CoreCorsPolicy", options =>
        {
            options.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }));

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddApiVersioning(options =>
        {
            options.UseApiBehavior = false;
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.Filters.Add(typeof(HandleErrorFilter));
                options.Filters.Add(new ValidationModelFilter());
                options.ModelBinderProviders.Insert(0, new ModelBinderProvider());
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.Converters.Insert(0, new TrimmingConverter());
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        },
                        Scheme = "oauth2",
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
            options.OperationFilter<CancellationTokenFilter>();
            // options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            // set the comments path for the Swagger JSON and UI.
            var basePath = AppContext.BaseDirectory;
            var xmlPath = Path.Combine(basePath, @"CF.WebApi.xml");

            if (File.Exists(xmlPath))
                options.IncludeXmlComments(xmlPath);
        });

        services.AddControllersWithViews()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        ConfigureCompression(services);
        ConfigureAuthentication(services, configurationFactory.Create<OAuthConfig>());
    }

    public void ConfigureAuthentication(IServiceCollection services, OAuthConfig authConfig)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authConfig.ClientSecret));
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authConfig.Issuer,
            ValidateAudience = true,
            ValidAudience = authConfig.Issuer,
            ValidateLifetime = true,
            IssuerSigningKey = signingKey
        };

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = !_isDevelop;
                options.IncludeErrorDetails = true;
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
            });
        services.AddHttpConfig();
    }


    public void ConfigureCompression(IServiceCollection services)
    {
        services.AddResponseCompression(options => { options.Providers.Add<GzipCompressionProvider>(); });

        services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });
    }


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
        app.Use(async (context, next) =>
        {
            if (DefaultRoutes.Contains(context.Request.Path.Value.ToLowerInvariant()))
            {
                context.Response.Redirect("/api/index.html");
                return;
            }

            await next();
        });
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors("CoreCorsPolicy");
        app.UseAuthentication();
        app.UseResponseCompression();
        app.UseAuthorization();
        app.UseEndpoints(c => c.MapControllers());

        app.UseSwagger(c =>
        {
            c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                swaggerDoc.Servers = new List<OpenApiServer>
                {
                    new()
                    {
                        Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/"
                    }
                };
            });
        });
        app.UseDeveloperExceptionPage();
        app.UseSwaggerUI(c =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
                c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

            c.RoutePrefix = "api";
            c.DocumentTitle = "CROWDFEEDING API";
        });
    }
}