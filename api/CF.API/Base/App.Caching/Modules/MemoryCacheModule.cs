using Autofac;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace App.Caching.Modules; 

public class MemoryCacheModule : Module {
    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);

        builder.RegisterInstance<IDistributedCache>(
            new MemoryDistributedCache(new OptionsWrapper<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions())));
    }
}