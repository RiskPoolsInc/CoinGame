using App.Interfaces.ExternalServices;

using Autofac;

namespace App.Infrastructure.VirtualWalletService.Modules;

public class VirtualWallerServiceModule : Module {
    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);
        builder.RegisterType<VirtualWallerService>().As<IWalletService>();
    }
}