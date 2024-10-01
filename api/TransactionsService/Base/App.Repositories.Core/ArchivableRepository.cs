using System.Linq.Expressions;

using App.Data.Sql.Core.Interfaces;
using App.Interfaces.Data.Entities;
using App.Interfaces.Repositories.Core;

using Z.EntityFramework.Plus;

namespace App.Repositories.Core;

public abstract class ArchivableRepository<TEntity, TKey> : AuditableRepository<TEntity, TKey>, IArchivableRepository<TEntity, TKey>
    where TEntity : class, IArchivableEntity<TKey>
    where TKey : struct {
    public ArchivableRepository(IDbContext context) : base(context) {
    }

    public override IQueryable<TEntity> GetAll() {
        return base.GetAll().Where(a => a.IsDeleted == false);
    }

    public IQueryable<TEntity> GetAllWithDeleted() {
        return base.GetAll();
    }

    public override Task RemoveWhereAsync(Expression<Func<TEntity, bool>> criteria,
                                          CancellationToken               cancellationToken) {
        return GetAll().Where(criteria).UpdateAsync(GetInstance(), cancellationToken);
    }

    public Task PermanentRemoveRangeAsync(Expression<Func<TEntity, bool>> criteria,
                                          CancellationToken               cancellationToken) {
        return GetAllWithDeleted().Where(criteria).DeleteAsync(cancellationToken);
    }

    public override void Delete(TEntity entity) {
        entity.IsDeleted = true;
        entity.DateDeleted = DateTime.UtcNow;
        Update(entity);
    }

    public void Recover(TKey id) {
        var entity = Set.Find(new object[] { id });

        if (entity == null)
            return;
        entity.IsDeleted = false;
        Update(entity);
    }

    public async Task RecoverAsync(TKey id, CancellationToken cancellationToken) {
        var entity = await Set.FindAsync(new object[] { id }, cancellationToken);

        if (entity == null)
            return;
        entity.IsDeleted = false;
        Update(entity);
    }

    public override Task ClearAsync(CancellationToken cancellationToken) {
        return GetAllWithDeleted().DeleteAsync(cancellationToken);
    }

    public IQueryable<TEntity> WhereWithDeleted(Expression<Func<TEntity, bool>> criteria) {
        return GetAllWithDeleted().Where(criteria);
    }

    public static Expression<Func<TEntity, TEntity>> GetInstance() {
        var t = typeof(TEntity);
        var val = Expression.Constant(true);
        var instance = Expression.Parameter(t);
        var newExp = Expression.New(t);
        var item1Member = t.GetMember("IsDeleted")[0];

        MemberBinding item1MemberBinding =
            Expression.Bind(item1Member, val);
        var memberInitExpression = Expression.MemberInit(newExp, item1MemberBinding);
        var lambda = Expression.Lambda<Func<TEntity, TEntity>>(memberInitExpression, instance);
        return lambda;
    }
}

public abstract class ArchivableRepository<TEntity> : ArchivableRepository<TEntity, Guid>,
                                                      IArchivableRepository<TEntity>
    where TEntity : class, IArchivableEntity {
    public ArchivableRepository(IDbContext context) : base(context) {
    }
}