using System.Collections;

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace App.Data.Sql.Core.Interfaces; 

public interface IDbSet<TEntity> : IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable
    where TEntity : class {
    EntityEntry Attach(TEntity            entity);
    void Add(TEntity                      entity);
    void Remove(TEntity                   entity);
    Task AddAsync(TEntity                 entity, CancellationToken cancellationToken);
    TEntity Find(object[]                 keyValues);
    ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);
}