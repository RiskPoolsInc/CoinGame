using App.Interfaces.Core;

using Autofac;

using MediatR;

namespace App.Core.Pipeline.Modules;

public class DispatcherModule : Module {
    protected override void Load(ContainerBuilder builder) {
        builder.Register<ServiceFactory>(ctx => {
            var c = ctx.Resolve<IComponentContext>();
            return t => c.Resolve(t);
        });
        builder.RegisterType<BaseDispatcher>().As<IDispatcher>();
    }
}