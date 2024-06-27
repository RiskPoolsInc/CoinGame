using Autofac;

using FluentValidation;

namespace App.Core.Pipeline.Validators.Modules;

public class ValidationModule : Module {
    protected override void Load(ContainerBuilder builder) {
        ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

        var validators = ThisAssembly.GetTypes().Where(t => t.IsClosedTypeOf(typeof(IValidator<>))).ToArray();
        Array.ForEach(validators, v => builder.RegisterType(v).AsSelf().AsImplementedInterfaces());
        base.Load(builder);
    }
}