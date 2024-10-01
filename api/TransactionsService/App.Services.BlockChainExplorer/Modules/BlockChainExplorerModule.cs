using App.Services.BlockChainExplorer.Options;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace App.Services.BlockChainExplorer.Modules;

public class BlockChainExplorerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.Register(ctx => BlockChainExplorerProvider.Get(ctx.Resolve<IConfiguration>())).AsSelf()
            .AsImplementedInterfaces();
        builder.RegisterType<BlockChainExplorer>().AsImplementedInterfaces();
    }
}