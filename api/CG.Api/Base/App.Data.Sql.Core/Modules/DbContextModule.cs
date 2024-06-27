using System.Reflection;

using App.Data.Sql.Core.Configuration;
using App.Data.Sql.Core.Interfaces;

using Autofac;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Module = Autofac.Module;

namespace App.Data.Sql.Core.Modules;

public abstract class DbContextModule<TContext> : Module where TContext : BaseDbContext {
    private static IContainer _container;
    private readonly string _connectionStringName;
    private readonly string _contextName;

    public DbContextModule(string? connectionStringName) : this() {
        _connectionStringName = connectionStringName ?? typeof(TContext).Name;
    }

    public DbContextModule() {
        _connectionStringName ??= _connectionStringName ?? null;
        _contextName = typeof(TContext).Name;
    }

    protected override Assembly ThisAssembly => GetType().Assembly;

    public TContext CreateDbContext(string[] args) {
        var builder = new ContainerBuilder();
        builder.RegisterInstance(GetConfiguration());
        Load(builder);
        builder.RegisterType<TContext>();
        _container = builder.Build();
        return _container.Resolve<TContext>();
    }

    protected override void Load(ContainerBuilder builder) {
        DbConfigurator.Register(ThisAssembly);

        builder.Register(c => {
                    var config = c.Resolve<IConfiguration>();
                    var optionsBuilder = new DbContextOptionsBuilder<TContext>();

                    optionsBuilder.UseNpgsql(config.GetConnectionString(_connectionStringName ?? _contextName), a => {
                        a.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
                        a.EnableRetryOnFailure(3);
                        a.MigrationsAssembly(ThisAssembly.FullName);
                        a.MigrationsHistoryTable($"__{_contextName}Migrations");
                        a.UseNetTopologySuite();
                    });
                    return optionsBuilder.Options;
                })
               .AsSelf()
               .SingleInstance();
        RegisterContext(builder);
        builder.RegisterGeneric(typeof(DbConnectionAdapter<>)).As(typeof(IDbConnectionAdapter<>));
        base.Load(builder);
    }

    protected virtual void RegisterContext(ContainerBuilder builder, params Type[] constructorArgs) {
        var registrationBuilder = builder.RegisterType<TContext>()
                                         .AsImplementedInterfaces()
                                         .InstancePerLifetimeScope();

        if (constructorArgs.Length > 0)
            registrationBuilder.UsingConstructor(constructorArgs);
    }

    private IConfiguration GetConfiguration() {
        return new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
    }
}