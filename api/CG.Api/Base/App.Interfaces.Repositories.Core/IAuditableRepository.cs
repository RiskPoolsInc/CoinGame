using App.Interfaces.Data.Entities;

namespace App.Interfaces.Repositories.Core;

public interface IAuditableRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IAuditableEntity<TKey> {
}

public interface IAuditableRepository<TEntity> : IAuditableRepository<TEntity, Guid> where TEntity : class, IAuditableEntity {
}