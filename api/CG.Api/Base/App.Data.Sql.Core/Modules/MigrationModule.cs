using App.Data.Sql.Core.Interfaces;
using App.Data.Sql.Core.Migrations;

using Autofac;

namespace App.Data.Sql.Core.Modules;

public class MigrationModule : Module {
    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);
        builder.RegisterType<MigrationManager>().As<IMigrationManager>();
    }
}