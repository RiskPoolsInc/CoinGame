using System.Linq.Expressions;

using App.Interfaces.Data.Entities;

namespace App.Interfaces.Repositories.Core;

public interface IArchivableRepository<TEntity, TKey> : IAuditableRepository<TEntity, TKey> where TEntity : class, IArchivableEntity<TKey> {
    void Recover(TKey      id);
    Task RecoverAsync(TKey id, CancellationToken cancellationToken);
    IQueryable<TEntity> GetAllWithDeleted();
    IQueryable<TEntity> WhereWithDeleted(Expression<Func<TEntity, bool>> criteria);
    Task PermanentRemoveRangeAsync(Expression<Func<TEntity, bool>>       criteria, CancellationToken cancellationToken);
}

public interface IArchivableRepository<TEntity> : IArchivableRepository<TEntity, Guid> where TEntity : class, IArchivableEntity {
}