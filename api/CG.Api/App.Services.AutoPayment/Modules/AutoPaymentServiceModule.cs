using App.Core.Commands.AutoPaymentServiceCommands;
using App.Interfaces.Core;
using App.Services.AutoPayment.Options;

using Autofac;

using Microsoft.Extensions.Configuration;

namespace App.Services.AutoPayment.Modules;

public class AutoPaymentServiceModule : Module {
    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);

        builder.Register(ctx => ctx.Resolve<IConfiguration>()
                                   .GetSection(AutoPaymentServiceOptions.SECTION_NAME)
                                   .Get<AutoPaymentServiceOptions>())
               .As<IAutoPaymentServiceOptions>();

        builder.RegisterType<AutoPaymentService>().SingleInstance();
        builder.RegisterInstance(typeof(AutoPaymentService));
    }
}