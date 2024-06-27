using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using App.Common.Helpers;
using App.Data.Sql.Core.Interfaces;
using App.Interfaces.Data.Entities;
using App.Interfaces.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Z.EntityFramework.Plus;

namespace App.Repositories.Core;

public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    protected readonly IDbContext _context;

    protected BaseRepository(IDbContext context)
    {
        _context = context;
        Set = _context.Set<TEntity>();
    }

    public IDbSet<TEntity> Set { get; }

    public virtual IQueryable<TEntity> GetDbSet()
    {
        return Set.AsNoTracking();
    }

    public virtual void Add(TEntity entity)
    {
        Set.Add(entity);
    }

    public string SqlAddQuery(TEntity entity)
    {
        try
        {
            var entityType = typeof(TEntity);

            var fields = entityType.GetProperties()
                .Where(a => a.PropertyType is
                {
                    IsPublic: true,
                    IsValueType: true
                } or {Name: "String"});

            var contextType = _context.GetType();
            var members = contextType.GetProperties().Where(a => a.PropertyType.Name.Contains("DbSet"));

            var currentDbSetType =
                members.SingleOrDefault(
                    a => a.PropertyType.GenericTypeArguments.Any(
                        a => a == entityType || (entityType.BaseType != null && a == entityType.BaseType)));

            var id = entity.GetValue<Guid>("Id").ToString("D");

            var sqlEnd = @"
    END IF;
END
$do$;";

            var sql = $@"
DO
$do$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM ""{currentDbSetType.Name}"" WHERE ""Id""='{id}') then
        insert into ""{currentDbSetType.Name}"" (";

            foreach (var fieldInfo in fields)
                sql += $"\"{fieldInfo.Name}\", ";

            sql = sql.Substring(0, sql.Length - 2);
            sql += ") ";
            sql += " VALUES(";

            foreach (var fieldInfo in fields)
            {
                var value = fieldInfo.GetValue(entity);
                var stringValue = "";

                switch (value?.GetType().Name.ToLower() ?? "")
                {
                    case "datetime":
                        stringValue = value?.ToString().Replace(" ", "T") ?? "null";

                        if (stringValue != "null")
                            stringValue = $"'{stringValue}'";
                        break;
                    case "guid":
                    case "string":
                        stringValue = value == null
                            ? "null"
                            : "'" + value.ToString()?.Replace("'", "''") + "'";
                        break;
                    default:
                        stringValue = value?.ToString() ?? "null";
                        break;
                }

                sql += stringValue;
                sql += ", ";
            }

            sql = sql.Substring(0, sql.Length - 2);
            sql += ");";
            sql += sqlEnd;
            sql = sql.Replace(", ,", ", null,");
            return sql;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool Any(TKey id)
    {
        return Where(a => a.Id.Equals(id)).Any();
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
    {
        return Where(criteria).AnyAsync(cancellationToken);
    }

    public Task<bool> AllAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
    {
        return AllAsync(criteria, cancellationToken);
    }

    public Task<bool> AnyAsync(TKey id, CancellationToken cancellationToken)
    {
        return AnyAsync(a => a.Id.Equals(id), cancellationToken);
    }

    public virtual bool Any(Expression<Func<TEntity, bool>> criteria)
    {
        return Where(criteria).Any();
    }

    public int Count()
    {
        return GetAll().Count();
    }

    public int Count(Expression<Func<TEntity, bool>> criteria)
    {
        return Where(criteria).Count();
    }

    public Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return GetAll().CountAsync(cancellationToken);
    }

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
    {
        return GetAll().CountAsync(criteria, cancellationToken);
    }

    public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return Set.AddAsync(entity, cancellationToken);
    }

    public virtual Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        return Task.WhenAll(entities.Select(a => AddAsync(a, cancellationToken)).ToArray());
    }

    public virtual void Update(TEntity entity)
    {
    }

    public virtual void UpdateWhere(Expression<Func<TEntity, bool>> criteria,
        Expression<Func<TEntity, TEntity>> updateFactory
    )
    {
        Where(criteria).Update(updateFactory);
    }

    public TEntity Update(TKey id,
        Action<TEntity> func,
        params Expression<Func<TEntity, dynamic>>[] propertyToUpdate
    )
    {
        var entity = CreateInstance(id);
        var entry = Set.Attach(entity);
        func(entity);

        if (propertyToUpdate.Length > 0)
            ForceModified(entry, propertyToUpdate);
        Update(entity);
        return entity;
    }

    public Task<TEntity> UpdateAsync(TKey id, Func<TEntity, Task> func)
    {
        var entity = CreateInstance(id);
        var entry = Set.Attach(entity);

        return func(entity)
            .ContinueWith(task =>
            {
                Update(entity);
                return entity;
            });
    }

    public void Delete(TKey id)
    {
        var entity = CreateInstance(id);
        Set.Attach(entity);
        Delete(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        Set.Remove(entity);
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return Set.AsNoTracking();
    }

    public virtual ValueTask<TEntity> FindAsync(TKey id, CancellationToken cancellationToken)
    {
        return Set.FindAsync(new object[] {id}, cancellationToken);
    }

    public Task<TEntity> FindWithIncludeAsync(TKey id,
        CancellationToken cancellationToken,
        params Expression<Func<TEntity, IEnumerable>>[] propertyExpressions
    )
    {
        var query = Filter(Set, s => s.Id, id);

        foreach (var propertyExpression in propertyExpressions.AsNotNull())
            query = query.Include(propertyExpression);

        var entity = query.FirstOrDefaultAsync(cancellationToken);

        return entity;
    }

    public virtual IQueryable<TEntity> Get(TKey id)
    {
        return GetAll().Where(a => a.Id.Equals(id));
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    public virtual Task<int> UpdateWhereAsync(Expression<Func<TEntity, bool>> criteria,
        Expression<Func<TEntity, TEntity>> updateFactory,
        CancellationToken cancellationToken
    )
    {
        return Where(criteria).UpdateAsync(updateFactory, cancellationToken);
    }

    public Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> criteria)
    {
        return GetAll().Where(criteria);
    }

    public virtual Task RemoveWhereAsync(Expression<Func<TEntity, bool>> criteria,
        CancellationToken cancellationToken
    )
    {
        return Where(criteria).DeleteAsync(cancellationToken);
    }

    public virtual Task ClearAsync(CancellationToken cancellationToken)
    {
        return GetAll().DeleteAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected IDbSet<T> CustomContext<T>() where T : class
    {
        return _context.Set<T>();
    }

    public virtual async Task AddOrUpdate(TEntity entity, CancellationToken cancellationToken)
    {
        if (!await AnyAsync(entity.Id, cancellationToken))
            await AddAsync(entity, cancellationToken);
        else
            Update(entity);
    }

    protected ChangeTracker GetChangeTracker()
    {
        return _context.ChangeTracker;
    }

    protected virtual TEntity CreateInstance(TKey id)
    {
        var entity = Activator.CreateInstance<TEntity>();
        entity.Id = id;
        return entity;
    }

    private void ForceModified(EntityEntry entry, Expression<Func<TEntity, dynamic>>[] properties)
    {
        var list = properties.Select(a => GetPropertyByExpression(a.Body)).ToArray();
        Array.ForEach(list, p => entry.Property(p).IsModified = true);
    }

    private string GetPropertyByExpression(Expression expression)
    {
        if (expression is UnaryExpression)
            expression = (expression as UnaryExpression).Operand;

        if (expression is MemberExpression)
        {
            var member = expression as MemberExpression;
            return member.Member.Name;
        }

        throw new NotSupportedException("MemberExpressions are supported only.");
    }

    protected static IQueryable<TEntry> Filter<TEntry, TProperty>(IQueryable<TEntry> dbSet,
        Expression<Func<TEntry, TProperty>> property,
        TProperty value
    )
        where TProperty : struct
    {
        var memberExpression = property.Body as MemberExpression;

        if (memberExpression == null || !(memberExpression.Member is PropertyInfo))
            throw new ArgumentException("Property expected", "property");

        var left = property.Body;
        Expression right = Expression.Constant(value, typeof(TProperty));

        Expression searchExpression = Expression.Equal(left, right);
        var lambda = Expression.Lambda<Func<TEntry, bool>>(searchExpression, property.Parameters.Single());

        return dbSet.Where(lambda);
    }

    protected void Dispose(bool flag)
    {
        if (flag)
            if (_context != null)
                _context.Dispose();
    }

    ~BaseRepository()
    {
        Dispose(false);
    }
}