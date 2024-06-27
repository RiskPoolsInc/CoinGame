using System.Linq.Expressions;

using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace App.Common.Helpers;

public static class QueryableHelper {
    public static TResult[] ToArray<TSource, TResult>(this IQueryable<TSource> source) {
        return source.ProjectTo<TResult>().ToArray();
    }

    public static Task<TResult[]> ToArrayAsync<TSource, TResult>(this IQueryable<TSource> source, CancellationToken cancellationToken) {
        return source.ProjectTo<TResult>().ToArrayAsync(cancellationToken);
    }

    public static TResult Single<TSource, TResult>(this IQueryable<TSource> source) {
        return source.ProjectTo<TResult>().SingleOrDefault();
    }

    public static Task<TResult> SingleAsync<TSource, TResult>(this IQueryable<TSource> source, CancellationToken cancellationToken) {
        return source.ProjectTo<TResult>().SingleOrDefaultAsync(cancellationToken);
    }

    public static TResult First<TSource, TResult>(this IQueryable<TSource> source) {
        return source.ProjectTo<TResult>().FirstOrDefault();
    }

    public static Task<TResult> FirstAsync<TSource, TResult>(this IQueryable<TSource> source, CancellationToken cancellationToken) {
        return source.ProjectTo<TResult>().FirstOrDefaultAsync(cancellationToken);
    }

    public static Task<TResult> FirstAsync<TSource, TResult>(this IQueryable<TSource>        source,
                                                             Expression<Func<TResult, bool>> predicate,
                                                             CancellationToken               cancellationToken) {
        return source.ProjectTo<TResult>().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public static IOrderedQueryable<TSource> OrderByDirection<TSource, TKey>(this IQueryable<TSource>        source,
                                                                             Expression<Func<TSource, TKey>> selector,
                                                                             bool                            descending) {
        return descending
            ? source.OrderByDescending(selector)
            : source.OrderBy(selector);
    }

    public static IOrderedQueryable<TSource> OrderByDirection<TSource>(this IQueryable<TSource> source, string property, bool descending) {
        return descending
            ? source.OrderByDescending(GetLambda<TSource>(property))
            : source.OrderBy(GetLambda<TSource>(property));
    }

    public static Expression<Func<T, TValue>> SelectorDynamic<T, TValue>(string selector) {
        var parameter = Expression.Parameter(typeof(T), "p");
        var body = Expression.Constant(selector);
        return Expression.Lambda<Func<T, TValue>>(body, parameter);
    }

    private static Expression<Func<TSource, object>> GetLambda<TSource>(string propertyName) {
        var param = Expression.Parameter(typeof(TSource), "p");
        var body = param.GetProperty(propertyName);
        return Expression.Lambda<Func<TSource, object>>(Expression.Convert(body, typeof(object)), param);
    }
}