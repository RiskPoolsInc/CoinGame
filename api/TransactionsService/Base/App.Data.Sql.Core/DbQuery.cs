using System.Collections;
using System.Linq.Expressions;

using App.Data.Sql.Core.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace App.Data.Sql.Core;

public class DbQuery<TEntity> : IDbSet<TEntity> where TEntity : class {
    private readonly DbSet<TEntity> _set;

    public DbQuery(DbSet<TEntity> set) {
        _set = set;
        _query = set;
    }

    public EntityEntry Attach(TEntity entity) {
        return _set.Attach(entity);
    }

    public void Add(TEntity entity) {
        _set.Add(entity);
    }

    public void Remove(TEntity entity) {
        _set.Remove(entity);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken) {
        await _set.AddAsync(entity, cancellationToken);
    }

    public TEntity Find(object[] keyValues) {
        return _set.Find(keyValues);
    }

    public ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken) {
        return _set.FindAsync(keyValues, cancellationToken);
    }

    #region IQueriable

    private readonly IQueryable<TEntity> _query;

    public IEnumerator<TEntity> GetEnumerator() {
        return _query.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public Type ElementType => _query.ElementType;
    public Expression Expression => _query.Expression;
    public IQueryProvider Provider => _query.Provider;

    #endregion
}