using Autofac;

using Microsoft.Extensions.Caching.Distributed;

namespace App.Caching.Modules; 

public class EmptyCacheModule : Module {
    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);
        builder.RegisterType<EmptyCache>().As<IDistributedCache>();
    }
}