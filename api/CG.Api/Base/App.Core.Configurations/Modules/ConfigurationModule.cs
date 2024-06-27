using App.Core.Configurations.Factory;
using App.Interfaces.Core.Configurations;

using Autofac;

using Microsoft.Extensions.Configuration;

namespace App.Core.Configurations.Modules;

public class ConfigurationModule : Module {
    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);
        builder.Register(c => new ConfigFactory(c.Resolve<IConfiguration>())).As<IConfigFactory>().SingleInstance();
    }
}