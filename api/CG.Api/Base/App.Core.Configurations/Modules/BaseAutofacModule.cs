using System.Reflection;

using Autofac;

using Module = Autofac.Module;

namespace App.Core.Configurations.Modules;

public abstract class BaseAutofacModule<T> : Module {
    protected abstract Func<Type, bool> RegisterTypesFilter { get; }
    protected virtual Assembly CurrentAssembly => typeof(T).Assembly;

    protected override void Load(ContainerBuilder builder) {
        var types = CurrentAssembly.GetTypes().Where(RegisterTypesFilter).ToArray();
        builder.RegisterTypes(types).AsSelf().AsImplementedInterfaces();
        base.Load(builder);
    }
}