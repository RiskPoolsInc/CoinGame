using App.Data.Sql.Core.Providers;
using App.Data.Sql.MigrateAfterBuild;

using Autofac;

using MediatR;

namespace App.Data.Sql.Modules;

public class MigrateAfterBuildModule<T, TResult> : Module where T : IRequest<TResult>, new() {
    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);
        builder.RegisterType<RunMigrateAfterBuild>().AsSelf().AsImplementedInterfaces();
        builder.RegisterType<RunMigrateAfterBuildHandler>().AsSelf().AsImplementedInterfaces();
        builder.RegisterBuildCallback(BuildCallback);
    }

    private async void BuildCallback(ILifetimeScope scope) {
        var migrateAfterBuild = scope.Resolve<MigrationConfigProvider>();

        if (migrateAfterBuild?.MigrateAfterBuild ?? false) {
            var handler = scope.Resolve<IRequestHandler<T, TResult>>();
            await handler.Handle(new T(), CancellationToken.None);
        }
    }
}