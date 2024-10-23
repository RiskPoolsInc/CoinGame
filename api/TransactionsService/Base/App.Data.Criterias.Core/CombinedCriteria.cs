using System.Linq.Expressions;

using App.Common.Helpers;
using App.Data.Criterias.Core.Interfaces;

namespace App.Data.Criterias.Core;

public class CombinedCriteria<TEntity> : ICriteria<TEntity> where TEntity : class {
    private readonly ICriteria<TEntity> _from;
    private readonly ICriteria<TEntity> _to;

    public CombinedCriteria(ICriteria<TEntity> from, ICriteria<TEntity> to) {
        _from = from;
        _to = to;
    }

    public Expression<Func<TEntity, bool>> Build() {
        return _from.Build().And(_to.Build());
    }
}