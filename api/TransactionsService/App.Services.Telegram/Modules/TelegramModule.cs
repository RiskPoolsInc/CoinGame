using App.Services.Telegram.Options;

using Autofac;

using Microsoft.Extensions.Configuration;

namespace App.Services.Telegram.Modules; 

public class TelegramModule : Module {
    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);
        builder.Register(ctx => TelegramOptionsProvider.Get(ctx.Resolve<IConfiguration>())).AsSelf().AsImplementedInterfaces();
        builder.RegisterType<TelegramClient>();
    }
}