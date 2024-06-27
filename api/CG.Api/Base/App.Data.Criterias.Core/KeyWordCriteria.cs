using System.Linq.Expressions;

using App.Common.Helpers;

namespace App.Data.Criterias.Core;

public abstract class KeyWordCriteria<TEntity> : PagedCriteria<TEntity> where TEntity : class {
    protected KeyWordFilter[] Keywords { get; set; }

    public override Expression<Func<TEntity, bool>> Build() {
        Expression<Func<TEntity, bool>> result = a => true;

        if (Keywords?.Length > 0)
            return Keywords.Select(filter => BuildKeywordFilter<TEntity>(filter.KeyWord, filter.KeyWordProperties))
                           .Aggregate(result, (current, cur) => current.And(cur));

        return result;
    }

    private Expression<Func<TEntity, bool>> BuildKeywordFilter<T>(string keyword, IEnumerable<string> keyWordProperties) {
        Expression<Func<TEntity, bool>> result = a => false;

        if (keyWordProperties?.Count() > 0)
            return keyWordProperties.Select(name => CreateContainsExpression<TEntity>(keyword, name))
                                    .Aggregate(result, (current, cur) => current.Or(cur));

        return result;
    }

    private static Expression<Func<TEntity, bool>> CreateContainsExpression<T>(string keyword, string propertyName) {
        var keywordExpr = Expression.Constant(keyword, typeof(string));
        var param = Expression.Parameter(typeof(T), "x");
        Expression body = param;

        propertyName = propertyName.FromCamelCaseToTitleCase();

        body = propertyName.Split('.')
                           .Aggregate(body, (current, member) => Expression.Property(current, member));

        var lower = Expression.Call(body, "ToLower", null);
        var contains = Expression.Call(lower, "Contains", null, keywordExpr);
        return Expression.Lambda<Func<TEntity, bool>>(contains, param);
    }
}