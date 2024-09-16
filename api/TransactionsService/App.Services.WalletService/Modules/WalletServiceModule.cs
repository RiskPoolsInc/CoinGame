using App.Services.Telegram.Options;

using Autofac;

using Microsoft.Extensions.Configuration;

namespace App.Services.WalletService.Modules;

public class WalletServiceModule : Module {
    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);
        builder.RegisterType<WalletService>().As<IWalletService>();

        builder.Register(ctx => ctx.Resolve<IConfiguration>()
                                   .GetSection(WalletServiceOptions.SECTION_NAME)
                                   .Get<WalletServiceOptions>());
    }
}