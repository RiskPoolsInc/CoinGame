using System.Linq.Expressions;
using System.Reflection;

using App.Common.Helpers;
using App.Data.Criterias.Core.Helpers;

namespace App.Data.Criterias.Core;

public class FilterCriteria<TEntity, T> : PagedCriteria<TEntity> where TEntity : class {
    private readonly string _propertyName;
    private readonly StateArrayExcludeEnum _type;
    private readonly T[] _values;

    public FilterCriteria(StateArrayExcludeEnum type, string propertyName, T[] values) {
        _type = type;
        _propertyName = propertyName;
        _values = values;
    }

    public override Expression<Func<TEntity, bool>> Build() {
        if (typeof(T) == typeof(DateTime) && _values.Length == 2)
            return CreateRangeExpression(_propertyName, _values);

        return _type switch {
            StateArrayExcludeEnum.ArrayAndExclude       => CreateArrayAndExcludeExpression(_propertyName, _values),
            StateArrayExcludeEnum.ArrayAndNotExclude    => CreateArrayAndNotExcludeExpression(_propertyName, _values),
            StateArrayExcludeEnum.NotArrayAndExclude    => CreateNotArrayAndExcludeExpression(_propertyName, _values),
            StateArrayExcludeEnum.NotArrayAndNotExclude => CreateNotArrayAndNotExcludeExpression(_propertyName, _values),
            _                                           => a => true
        };
    }

    private static Expression<Func<TEntity, bool>> CreateArrayAndExcludeExpression<T>(string propertyName, T[] propertyValues) {
        var valueExpr = Expression.Constant(propertyValues, typeof(T[]));
        var (param, property) = GetPropertyNameAndValueExpression(propertyName);

        var contains = GetContainsMethod<T>();

        var body = Expression.Not(Expression.Call(contains, valueExpr, property));
        return Expression.Lambda<Func<TEntity, bool>>(body, param);
    }

    private static Expression<Func<TEntity, bool>> CreateArrayAndNotExcludeExpression<T>(string propertyName, T[] propertyValues) {
        var valueExpr = Expression.Constant(propertyValues, typeof(T[]));
        var (param, property) = GetPropertyNameAndValueExpression(propertyName);

        var contains = GetContainsMethod<T>();

        var body = Expression.Call(contains, valueExpr, property);
        return Expression.Lambda<Func<TEntity, bool>>(body, param);
    }

    private static Expression<Func<TEntity, bool>> CreateNotArrayAndExcludeExpression<T>(string propertyName, T[] propertyValues) {
        var valueExpr = Expression.Constant(propertyValues[0], typeof(T));
        var (param, property) = GetPropertyNameAndValueExpression(propertyName);

        var notEquals = Expression.Not(Expression.Equal(property, valueExpr));
        return Expression.Lambda<Func<TEntity, bool>>(notEquals, param);
    }

    private static Expression<Func<TEntity, bool>> CreateNotArrayAndNotExcludeExpression<T>(string propertyName, T[] propertyValues) {
        var valueExpr = Expression.Constant(propertyValues[0], typeof(T));
        var (param, property) = GetPropertyNameAndValueExpression(propertyName);

        var equals = Expression.Equal(property, valueExpr);

        return Expression.Lambda<Func<TEntity, bool>>(equals, param);
    }

    private static Expression<Func<TEntity, bool>> CreateRangeExpression<T>(string propertyName, T[] propertyValues) {
        var valueExpr = propertyValues.Select(s => Expression.Constant(s, typeof(T))).ToArray();
        var (param, property) = GetPropertyNameAndValueExpression(propertyName);

        var body =
            Expression.And(Expression.GreaterThanOrEqual(property, valueExpr[0]), Expression.LessThanOrEqual(property, valueExpr[1]));

        return Expression.Lambda<Func<TEntity, bool>>(body, param);
    }

    private static (ParameterExpression, Expression) GetPropertyNameAndValueExpression(string propertyName) {
        var paramExpr = Expression.Parameter(typeof(TEntity), "x");
        Expression body = paramExpr;

        propertyName = propertyName.FromCamelCaseToTitleCase();

        body = propertyName.Split('.')
                           .Aggregate(body, (current, member) => Expression.Property(current, member));

        return (paramExpr, body);
    }

    private static MethodInfo GetContainsMethod<T>() {
        var contains = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                         .Single(x => x.Name == "Contains" && x.GetParameters().Length == 2)
                                         .MakeGenericMethod(typeof(T));

        return contains;
    }
}