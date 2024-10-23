using System.Linq.Expressions;

using App.Common.Helpers;
using App.Data.Criterias.Core.Interfaces;

namespace App.Data.Criterias.Core;

public class OtherwiseCriteria<TEntity> : ICriteria<TEntity> where TEntity : class {
    private readonly ICriteria<TEntity> _left;
    private readonly ICriteria<TEntity> _right;

    public OtherwiseCriteria(ICriteria<TEntity> left, ICriteria<TEntity> right) {
        _left = left;
        _right = right;
    }

    public Expression<Func<TEntity, bool>> Build() {
        return _left.Build().Or(_right.Build());
    }
}