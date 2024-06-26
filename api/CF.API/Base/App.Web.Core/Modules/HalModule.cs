using App.Web.Core.HAL;
using App.Web.Core.HAL.Generators;
using App.Web.Core.Metadata;

using Autofac;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace App.Web.Core.Modules;

public class HalModule : Module {
    protected override void Load(ContainerBuilder builder) {
        builder.RegisterType<Catalog>().AsSelf().SingleInstance();
        builder.RegisterType<ObjectSchemaGenerator>().As<ISchemaGenerator>().SingleInstance();
        builder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().SingleInstance();

        builder.Register(x => {
                    var actionContext = x.Resolve<IActionContextAccessor>().ActionContext;
                    var urlHelperFactory = x.Resolve<IUrlHelperFactory>();
                    var urlHelper = urlHelperFactory.GetUrlHelper(actionContext);
                    return new PathGenerator(actionContext.HttpContext, urlHelper);
                })
               .As<IPathGenerator>()
               .InstancePerLifetimeScope();
        builder.RegisterType<HalFactory>().As<IHalFactory>();
        base.Load(builder);
    }
}