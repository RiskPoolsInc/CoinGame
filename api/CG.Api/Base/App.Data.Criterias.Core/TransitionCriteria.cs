using System.Linq.Expressions;

using App.Common.Helpers;
using App.Data.Criterias.Core.Interfaces;

namespace App.Data.Criterias.Core;

public class TransitionCriteria<TFrom, TTo> : ICriteria<TTo>
    where TTo : class
    where TFrom : class {
    private readonly Expression<Func<TTo, TFrom>> _selector;
    private readonly ICriteria<TFrom> _source;

    public TransitionCriteria(ICriteria<TFrom> criteria, Expression<Func<TTo, TFrom>> selector) {
        _source = criteria;
        _selector = selector;
    }

    public Expression<Func<TTo, bool>> Build() {
        return _source.Build().Convert(_selector);
    }
}