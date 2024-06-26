using App.Services.UbikiriApiService.Configurations;

using Autofac;

using Microsoft.Extensions.Configuration;

namespace App.Services.UbikiriApiService.Modules;

public class UbikiriApiModule : Module {
    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);

        builder.Register(ctx =>
                             ctx.Resolve<IConfiguration>().GetSection(UbikiriSettings.SECTION_NAME).Get<UbikiriSettings>());
    }
}