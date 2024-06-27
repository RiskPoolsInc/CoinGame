using System.Collections;
using System.Linq.Expressions;
using App.Interfaces.Data.Entities;

namespace App.Interfaces.Repositories.Core;

public interface IListRepository<out TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
}

public interface IRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>, IListRepository<TEntity>, IDisposable
    where TEntity : class, IEntity<TKey>
{
    #region Methods

    IQueryable<TEntity> GetDbSet();
    bool Any(TKey id);
    bool Any(Expression<Func<TEntity, bool>> criteria);
    int Count();
    int Count(Expression<Func<TEntity, bool>> criteria);
    IQueryable<TEntity> Get(TKey id);
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> criteria);
    void Add(TEntity entity);
    void Update(TEntity entity);
    TEntity Update(TKey id, Action<TEntity> func, params Expression<Func<TEntity, dynamic>>[] propertyToUpdate);
    Task<TEntity> UpdateAsync(TKey id, Func<TEntity, Task> func);
    void Delete(TKey id);
    void Delete(TEntity entity);
    int Save();
    string SqlAddQuery(TEntity entity);

    #endregion

    #region Async Methods

    ValueTask<TEntity> FindAsync(TKey id, CancellationToken cancellationToken);

    Task<TEntity> FindWithIncludeAsync(TKey id,
        CancellationToken cancellationToken,
        params Expression<Func<TEntity, IEnumerable>>[] propertyExpressions
    );

    Task<bool> AnyAsync(TKey id, CancellationToken cancellationToken);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);
    Task<bool> AllAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    Task<int> SaveAsync(CancellationToken cancellationToken);

    Task<int> UpdateWhereAsync(Expression<Func<TEntity, bool>> criteria,
        Expression<Func<TEntity, TEntity>> updateFactory,
        CancellationToken cancellationToken
    );

    void UpdateWhere(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TEntity>> updateFactory);
    Task RemoveWhereAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);
    Task ClearAsync(CancellationToken cancellationToken);

    #endregion
}

public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : class, IEntity
{
}