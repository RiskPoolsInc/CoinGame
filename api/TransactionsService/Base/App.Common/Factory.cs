using Autofac;

namespace App.Common;

public class Factory<TOut> : IFactory<TOut> {
    private readonly IComponentContext _componentContext;

    public Factory(IComponentContext componentContext) {
        _componentContext = componentContext ?? throw new ArgumentNullException(nameof(componentContext));
    }

    TOut IFactory<TOut>.Create() {
        return Resolve();
    }

    protected TOut Resolve(params TypedParameter[] parameters) {
        return _componentContext.Resolve<TOut>(parameters);
    }
}

public class Factory<TParam, TOut> : Factory<TOut>, IFactory<TParam, TOut> {
    public Factory(IComponentContext componentContext) : base(componentContext) {
    }

    TOut IFactory<TParam, TOut>.Create(TParam parameter) {
        return Resolve(new TypedParameter(typeof(TParam), parameter));
    }
}

public class Factory<TParam1, TParam2, TOut> : Factory<TOut>, IFactory<TParam1, TParam2, TOut> {
    public Factory(IComponentContext componentContext) : base(componentContext) {
    }

    TOut IFactory<TParam1, TParam2, TOut>.Create(TParam1 param1, TParam2 param2) {
        return Resolve(new TypedParameter(typeof(TParam1), param1), new TypedParameter(typeof(TParam2), param2));
    }
}

public interface IFactory<TParam1, TParam2, out TOut> {
    TOut Create(TParam1 param1, TParam2 param2);
}

public interface IFactory<TParam, out TOut> {
    TOut Create(TParam parameter);
}

public interface IFactory<out TOut> {
    TOut Create();
}