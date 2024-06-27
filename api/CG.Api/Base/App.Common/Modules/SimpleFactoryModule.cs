using Autofac;

namespace App.Common.Modules;

public class SimpleFactoryModule : Module {
    protected override void Load(ContainerBuilder builder) {
        base.Load(builder);
        builder.RegisterGeneric(typeof(Factory<>)).As(typeof(IFactory<>)).SingleInstance();
        builder.RegisterGeneric(typeof(Factory<,>)).As(typeof(IFactory<,>)).SingleInstance();
        builder.RegisterGeneric(typeof(Factory<,,>)).As(typeof(IFactory<,,>)).SingleInstance();
    }
}