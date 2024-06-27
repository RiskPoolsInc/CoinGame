using App.Data.Sql.Core.Providers;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace App.Data.Sql.Core.Modules
{
    public class AppSettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(ctx => AppSettingProvider.Get(ctx.Resolve<IConfiguration>())).AsSelf()
                .AsImplementedInterfaces();
        }
    }
}