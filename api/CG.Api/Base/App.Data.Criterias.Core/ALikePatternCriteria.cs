using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace App.Data.Criterias.Core;

public abstract class ALikePatternCriteria<TEntity> : ACriteria<TEntity>
    where TEntity : class {
    public enum Verb {
        Start,
        Contains,
        End
    }

    protected readonly string _pattern;

    public ALikePatternCriteria(string searchString, Verb verb) {
        searchString = searchString?.Trim() ?? throw new ArgumentNullException(nameof(searchString));

        switch (verb) {
            case Verb.Start:
                _pattern = $"{searchString}%";
                break;
            case Verb.End:
                _pattern = $"%{searchString}";
                break;
            case Verb.Contains:
                _pattern = $"%{searchString}%";
                break;
        }
    }

    protected abstract Expression<Func<TEntity, string>> Property { get; }

    public override Expression<Func<TEntity, bool>> Build() {
        var entity = Expression.Parameter(typeof(TEntity), "entity");

        var propBody = Property.Body as MemberExpression;

        if (propBody == null)
            throw new NotSupportedException($"Supposed to be property expression of type MemberExpression, but provided {Property.Body.GetType().Name}");

        var property = Expression.Property(entity, propBody.Member.Name);

        return Expression.Lambda<Func<TEntity, bool>>(Expression.Call(typeof(DbFunctionsExtensions),
                                                                      nameof(DbFunctionsExtensions.Like),
                                                                      null,
                                                                      Expression.Constant(EF.Functions),
                                                                      property,
                                                                      Expression.Constant(_pattern)),
                                                      entity);
    }
}