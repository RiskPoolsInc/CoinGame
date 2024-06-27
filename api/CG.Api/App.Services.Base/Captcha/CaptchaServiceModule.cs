using Autofac;

using Microsoft.Extensions.Configuration;

namespace App.Services.Base.Captcha;

public class CaptchaServiceModule : Module {
    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);
        builder.Register(ctx => CaptchaOptionsProvider.Get(ctx.Resolve<IConfiguration>())).AsSelf().AsImplementedInterfaces();
        builder.RegisterType<CaptchaService>().AsSelf().SingleInstance();
    }
}