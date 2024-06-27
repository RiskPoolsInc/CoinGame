using App.Data.Sql.Core.Interfaces;
using App.Interfaces.Data.Entities;
using App.Interfaces.Repositories.Core;

namespace App.Repositories.Core;

public abstract class Repository<TEntity> : BaseRepository<TEntity, Guid>, IRepository<TEntity> where TEntity : class, IEntity {
    public Repository(IDbContext context) : base(context) {
    }

    public override void Add(TEntity entity) {
        var utcNow = DateTime.UtcNow;
        entity.CreatedOn = entity.CreatedOn == default ? utcNow : entity.CreatedOn;
        entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
        base.Add(entity);
    }

    public override Task AddAsync(TEntity entity, CancellationToken cancellationToken) {
        var utcNow = DateTime.UtcNow;
        entity.CreatedOn = entity.CreatedOn == default ? utcNow : entity.CreatedOn;
        entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
        return base.AddAsync(entity, cancellationToken);
    }
}