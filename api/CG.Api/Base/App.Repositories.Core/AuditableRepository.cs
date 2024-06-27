using App.Data.Sql.Core.Interfaces;
using App.Interfaces.Data.Entities;
using App.Interfaces.Repositories.Core;

namespace App.Repositories.Core;

public abstract class AuditableRepository<TEntity, TKey> : BaseRepository<TEntity, TKey>, IAuditableRepository<TEntity, TKey>
    where TEntity : class, IAuditableEntity<TKey>
    where TKey : struct {
    public AuditableRepository(IDbContext context) : base(context) {
    }

    public override void Add(TEntity entity) {
        var utcNow = DateTime.UtcNow;
        entity.UpdatedOn = entity.UpdatedOn == default ? utcNow : entity.UpdatedOn;
        entity.CreatedOn = entity.CreatedOn == default ? utcNow : entity.CreatedOn;
        base.Add(entity);
    }

    public override Task AddAsync(TEntity entity, CancellationToken cancellationToken) {
        var utcNow = DateTime.UtcNow;
        entity.UpdatedOn = entity.UpdatedOn == default ? utcNow : entity.UpdatedOn;
        entity.CreatedOn = entity.CreatedOn == default ? utcNow : entity.CreatedOn;
        return base.AddAsync(entity, cancellationToken);
    }

    public override void Update(TEntity entity) {
        entity.UpdatedOn = DateTime.UtcNow;
        base.Update(entity);
    }
}

public abstract class AuditableRepository<TEntity> : Repository<TEntity>, IRepository<TEntity>
    where TEntity : class, IAuditableEntity {
    public AuditableRepository(IDbContext context) : base(context) {
    }

    public override void Add(TEntity entity) {
        base.Add(entity);
        entity.UpdatedOn = (entity as IAuditableEntity<Guid>).CreatedOn;
    }

    public override Task AddAsync(TEntity entity, CancellationToken cancellationToken) {
        var result = base.AddAsync(entity, cancellationToken);
        entity.UpdatedOn = entity.UpdatedOn == default ? (entity as IAuditableEntity<Guid>).CreatedOn : entity.UpdatedOn;
        return result;
    }

    public override void Update(TEntity entity) {
        entity.UpdatedOn = DateTime.UtcNow;
        base.Update(entity);
    }
}