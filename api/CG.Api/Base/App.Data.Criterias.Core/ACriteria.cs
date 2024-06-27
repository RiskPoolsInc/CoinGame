using System.Linq.Expressions;

using App.Data.Criterias.Core.Interfaces;

namespace App.Data.Criterias.Core;

public abstract class ACriteria<TEntity> : ICriteria<TEntity>
    where TEntity : class {
    public ICriteria<TEntity> True => new TrueCriteria<TEntity>();
    public ICriteria<TEntity> False => new FalseCriteria<TEntity>();
    public abstract Expression<Func<TEntity, bool>> Build();

    public static implicit operator Expression<Func<TEntity, bool>>(ACriteria<TEntity> entity) {
        return entity.Build();
    }
}