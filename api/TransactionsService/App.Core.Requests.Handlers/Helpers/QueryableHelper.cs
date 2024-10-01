using System.Linq.Expressions;

using App.Common.Helpers;
using App.Core.ViewModels;
using App.Data.Criterias.Core.Interfaces;
using App.Interfaces.Core.Requests;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Requests.Handlers.Helpers;

public static class QueryableHelper {
    public static Task<TResult[]> ToArrayAsync<TSource, TResult>(this IQueryable<TSource> source,
                                                                 ISortCriteria<TSource>   options,
                                                                 CancellationToken        cancellationToken) where TSource : class {
        return options.OrderBy(source).ProjectTo<TResult>().ToArrayAsync(cancellationToken);
    }

    public static IQueryable<TSource> ToLookupQueryAsync<TSource>(this IQueryable<TSource> source,
                                                                  IPagedCriteria<TSource>  options) where TSource : class {
        var page = options?.Page ?? 1;
        var size = options?.Size ?? 200;

        var orderedList = options != null ? options.OrderBy(source) : source;

        orderedList = options?.GetAll == true
            ? orderedList
            : orderedList
             .Skip(options.Skip ?? (page - 1) * size)
             .Take(size);

        return orderedList;
    }

    public static IQueryable<TSource> ToLookupQuery<TSource>(this IQueryable<TSource> source,
                                                             int?                     page = null,
                                                             int?                     size = null,
                                                             int?                     skip = null) where TSource : class {
        page ??= 1;
        size ??= 200;

        var orderedList = source;

        orderedList = orderedList.Skip(skip ?? (page.Value - 1) * size.Value).Take(size.Value);

        return orderedList;
    }

    public static async Task<IPagedList<TResult>> ToLookupAsync<TSource, TResult>(this IQueryable<TSource> source,
                                                                                 IPagedCriteria<TSource> options,
                                                                                 CancellationToken cancellationToken) where TSource : class
        where TResult : class {
        var page = options?.Page ?? 1;
        var size = options?.Size ?? 200;

        var orderedList = options != null ? options.OrderBy(source) : source;

        orderedList = options?.GetAll == true
            ? orderedList
            : orderedList
             .Skip(options.Skip ?? (page - 1) * size)
             .Take(size);

        var items = await orderedList
                         .ProjectTo<TResult>()
                         .ToArrayAsync(cancellationToken);
        return new PagedList<TResult>(items, source.Count(), page, size);
    }

    public static async Task<PagedList<TResult>> ToLookupAsync<TResult>(this IQueryable<TResult> source,
                                                                        IPagedCriteria<TResult>  options,
                                                                        CancellationToken        cancellationToken) where TResult : class {
        var page = options?.Page ?? 1;
        var size = options?.Size ?? 200;

        var orderedList = options != null ? options.OrderBy(source) : source;

        orderedList = options?.GetAll == true
            ? orderedList
            : orderedList
             .Skip(options.Skip ?? (page - 1) * size)
             .Take(size);

        var items = await orderedList
           .ToArrayAsync(cancellationToken);
        return new PagedList<TResult>(items, source.Count(), page, size);
    }

    public static async Task<PagedList<TResult>> ToLookupDynamicAsync<TSource, TResult>(this IQueryable         source,
                                                                                        IPagedCriteria<TSource> options,
                                                                                        string                  searchExpr,
                                                                                        CancellationToken       cancellationToken)
        where TSource : class
        where TResult : class {
        var page = options.Page ?? 1;
        var size = options.Size ?? 200;
        var skip = (page - 1) * size;

        var items = await options.OrderBy(source)
                                 .SkipDynamic(options.Skip ?? (page - 1) * size)
                                 .TakeDynamic(size)
                                 .ProjectTo<TResult>()
                                 .WhereDynamic(s => searchExpr)
                                 .ToArrayAsync(cancellationToken);

        return new PagedList<TResult>(items, items.Length, page, size);
    }

    public static IQueryable SelectDynamic(this IQueryable source, string propertyName) {
        if (source == null)
            throw new ArgumentNullException("source");

        var param = Expression.Parameter(source.ElementType, "p");
        var prop = param.GetProperty(propertyName);
        var typeArguments = new[] { source.ElementType, prop.Type };

        var call = Expression.Call(typeof(Queryable),
                                   "Select",
                                   typeArguments,
                                   source.Expression,
                                   prop);

        return source.Provider.CreateQuery(call);
    }

    public static IQueryable WhereDynamic(this IQueryable source, Func<IQueryable, Expression> expr) {
        if (source == null)
            throw new ArgumentNullException("source");

        var typeArguments = new[] { source.ElementType };

        var call = Expression.Call(typeof(Queryable),
                                   "Where",
                                   typeArguments,
                                   source.Expression,
                                   expr(source));

        return source.Provider.CreateQuery(call);
    }

    public static IQueryable SkipDynamic(this IQueryable source, int count) {
        if (source == null)
            throw new ArgumentNullException("source");

        var typeArguments = new[] { source.ElementType };

        var call = Expression.Call(typeof(Queryable),
                                   "Skip",
                                   typeArguments,
                                   source.Expression,
                                   Expression.Constant(count));

        return source.Provider.CreateQuery(call);
    }

    public static IQueryable TakeDynamic(this IQueryable source, int count) {
        if (source == null)
            throw new ArgumentNullException("source");

        var typeArguments = new[] { source.ElementType };

        var call = Expression.Call(typeof(Queryable),
                                   "Take",
                                   typeArguments,
                                   source.Expression,
                                   Expression.Constant(count));

        return source.Provider.CreateQuery(call);
    }

    public static Expression NotEqualDynamic<T>(this IQueryable source, string name, T value) {
        var param = Expression.Parameter(source.ElementType, "p");
        var prop = Expression.Property(param, name);

        var valueExpr = Expression.Constant(value, typeof(T));

        var body = Expression.NotEqual(prop, valueExpr);

        var func = typeof(Func<,>);
        var genericFunc = func.MakeGenericType(source.ElementType, typeof(bool));

        var expr = Expression.Lambda(genericFunc, body, param);

        return expr;
    }

    public static Expression NotEqualNullDynamic(this IQueryable source) {
        var param = Expression.Parameter(source.ElementType, "p");

        var body = Expression.NotEqual(param, Expression.Constant(null));

        var func = typeof(Func<,>);
        var genericFunc = func.MakeGenericType(source.ElementType, typeof(bool));

        var expr = Expression.Lambda(genericFunc, body, param);

        return expr;
    }

    public static async Task<int> CountDynamicAsync(this IQueryable source, CancellationToken cancellationToken) {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var typeArguments = new[] { source.ElementType };

        var call = Expression.Call(typeof(EntityFrameworkQueryableExtensions),
                                   "CountAsync",
                                   typeArguments,
                                   source.Expression,
                                   Expression.Constant(cancellationToken));

        var expr = Expression.Lambda<Func<Task<int>>>(call);

        var res = expr.Compile();
        return await ((Task<int>)res.DynamicInvoke())!;
    }
}