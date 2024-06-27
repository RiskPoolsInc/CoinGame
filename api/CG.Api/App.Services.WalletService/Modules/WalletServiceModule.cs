using App.Services.Telegram.Options;

using Autofac;

using Microsoft.Extensions.Configuration;

namespace App.Services.WalletService.Modules;

public class WalletServiceModule : Module {
    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);

        builder.Register(ctx => ctx.Resolve<IConfiguration>()
                                   .GetSection(SystemSettingsOptions.SECTION_NAME)
                                   .Get<SystemSettingsOptions>());

        builder.Register(ctx => ctx.Resolve<IConfiguration>()
                                   .GetSection(WalletServiceOptions.SECTION_NAME)
                                   .Get<WalletServiceOptions>());
    }
}