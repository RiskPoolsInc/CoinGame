using Autofac.Extensions.DependencyInjection;

namespace CG.WebApi; 

public class Program {
    public static void Main(string[] args) {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateWebHostBuilder(string[] args) {
        return Host.CreateDefaultBuilder(args)
                   .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                   .ConfigureAppConfiguration((context, builder) => {
                        builder.SetBasePath(context.HostingEnvironment.ContentRootPath);

                        if (args?.Length > 0)
                            builder.AddCommandLine(args);
                    })
                   .ConfigureLogging((context, builder) => {
                        builder.AddConfiguration(context.Configuration.GetSection("Logging"));
                        builder.AddConsole();
                        builder.AddDebug();
                    })
                   .ConfigureWebHostDefaults(webBuilder => {
                        webBuilder.UseKestrel(options => { options.AddServerHeader = false; })
                                  .UseIIS()
                                  .UseStartup<Startup>();
                    });
    }
}