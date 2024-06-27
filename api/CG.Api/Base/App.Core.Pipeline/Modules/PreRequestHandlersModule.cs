using App.Core.Pipeline.Behaviors;
using Autofac;
using MediatR;
using MediatR.Pipeline;

namespace App.Core.Pipeline.Modules;

public class PreRequestHandlersModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(AdminAccessBehavior<>)).As(typeof(IRequestPreProcessor<>));
        builder.RegisterGeneric(typeof(CustomerAccessBehavior<>)).As(typeof(IRequestPreProcessor<>));
        builder.RegisterGeneric(typeof(EntityExistBehavior<>)).As(typeof(IRequestPreProcessor<>));
        builder.RegisterGeneric(typeof(ValidationBehavior<>)).As(typeof(IRequestPreProcessor<>));
        builder.RegisterGeneric(typeof(LoggingBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
        base.Load(builder);
    }
}