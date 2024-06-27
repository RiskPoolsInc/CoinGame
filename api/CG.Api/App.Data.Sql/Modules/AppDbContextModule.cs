using App.Data.Sql.Core.Interfaces;
using App.Data.Sql.Core.Modules;
using App.Data.Sql.Core.Providers;
using App.Interfaces.Data.Sql;

using Autofac;

using Microsoft.EntityFrameworkCore.Design;

namespace App.Data.Sql.Modules;

public class AppDbContextModule : DbContextModule<AppDbContext>, IDesignTimeDbContextFactory<AppDbContext> {
    public AppDbContextModule(string? connectionStringSection = null) : base(connectionStringSection) {
    }

    public AppDbContextModule() {
    }

    protected override void RegisterContext(ContainerBuilder builder, params Type[] constructorArgs) {
        builder.RegisterType<ConnectionStringProvider>().As<IConnectionStringProvider>();
        builder.RegisterGeneric(typeof(AppDbContextProvider<>)).As(typeof(IAppDbContextProvider<>));
        builder.RegisterType<AppDbContextFactory>().AsImplementedInterfaces();
        builder.Register(c => c.Resolve<IAppDbContextFactory>().Create()).AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<AppDbContext>().As<IDbContext>().InstancePerDependency();
    }
}