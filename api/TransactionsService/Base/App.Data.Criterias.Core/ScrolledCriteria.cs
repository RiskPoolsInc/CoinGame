using System.Linq.Expressions;

using App.Data.Criterias.Core.Interfaces;

namespace App.Data.Criterias.Core;

public abstract class ScrolledCriteria<TEntity> : ACriteria<TEntity>, ISortOptions where TEntity : class {
    public int Size { get; set; }
    public int? Skip { get; set; }
    public string Sort { get; set; }
    public int Direction { get; set; }

    public void SetSortBy<TProperty>(Expression<Func<TEntity, TProperty>> selector) {
        var memberEpression = selector.Body as MemberExpression;

        if (memberEpression == null)
            throw new NotSupportedException("SortBy expression is not supported");
        Sort = memberEpression.Member.Name;
    }
}