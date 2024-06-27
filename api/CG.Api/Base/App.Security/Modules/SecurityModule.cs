using App.Interfaces.Security;
using App.Interfaces.Security.Issuer;
using App.Security.Context;
using App.Security.Issuer;

using Autofac;

using Microsoft.AspNetCore.Http;

namespace App.Security.Modules;

public class SecurityModule : Module {
    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);
        builder.RegisterInstance(new HttpContextAccessor()).As<IHttpContextAccessor>();
        builder.RegisterType<JwtManager>().As<IJwtManager>();
        builder.RegisterType<WebContextProvider>().As<IContextProvider>().InstancePerLifetimeScope();
    }
}