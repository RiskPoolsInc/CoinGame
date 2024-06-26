using Autofac;

using MediatR;

namespace App.Core.Configurations.Modules;

public class BaseRequestHandlersModule<T> : BaseAutofacModule<T> {
    protected override Func<Type, bool> RegisterTypesFilter { get; } = t => t.IsClosedTypeOf(typeof(IRequestHandler<,>));
}