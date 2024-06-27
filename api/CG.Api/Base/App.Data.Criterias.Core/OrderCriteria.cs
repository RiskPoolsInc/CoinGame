using System.Linq.Expressions;

using App.Common.Helpers;
using App.Data.Criterias.Core.Interfaces;

namespace App.Data.Criterias.Core;

public abstract class OrderCriteria<TEntity> : ACriteria<TEntity>, ISortCriteria<TEntity> where TEntity : class {
    protected const string DefaultSortBy = "Id";
    public string Sort { get; set; }
    public int Direction { get; set; }

    public virtual IQueryable<TEntity> OrderBy(IQueryable<TEntity> source) {
        return source.Provider.CreateQuery<TEntity>(OrderyByExpression(source));
    }

    public virtual IQueryable OrderBy(IQueryable source) {
        return source.Provider.CreateQuery(OrderyByExpression(source));
    }

    public void SetSortBy<TProperty>(Expression<Func<TEntity, TProperty>> selector) {
        var memberEpression = selector.Body as MemberExpression;

        if (memberEpression == null)
            throw new NotSupportedException("SortBy expression is not supported");
        Sort = memberEpression.Member.Name;
    }

    private IOrderedQueryable<TEntity> OrderByDirection<TKey>(IQueryable<TEntity> source, Expression<Func<TEntity, TKey>> keySelector) {
        if (Direction > 0)
            return source.OrderBy(keySelector);
        return source.OrderByDescending(keySelector);
    }

    private IOrderedQueryable<TEntity>
        ThenByDirection<TKey>(IOrderedQueryable<TEntity> source, Expression<Func<TEntity, TKey>> keySelector) {
        if (Direction > 0)
            return source.ThenBy(keySelector);
        return source.ThenByDescending(keySelector);
    }

    protected IQueryable<TEntity> OrderByDirection<TKey>(IQueryable<TEntity> source, params Expression<Func<TEntity, TKey>>[] keySelector) {
        if (!keySelector.Any())
            return source;

        var result = OrderByDirection(source, keySelector[0]);

        for (var i = 0; i < keySelector.Length; i++)
            result = ThenByDirection(result, keySelector[i]);
        return result;
    }

    private Expression OrderyByExpression(IQueryable source) {
        if (source == null)
            throw new ArgumentNullException("source");

        if (string.IsNullOrWhiteSpace(Sort))
            Sort = DefaultSortBy;

        var param = Expression.Parameter(source.ElementType, "p");
        var body = param.GetProperty(Sort);
        var orderByExp = Expression.Lambda(body, param);
        var typeArguments = new[] { source.ElementType, body.Type };

        return Expression.Call(typeof(Queryable),
                               Direction > 0 ? "OrderBy" : "OrderByDescending",
                               typeArguments, source.Expression, Expression.Quote(orderByExp));
    }
}