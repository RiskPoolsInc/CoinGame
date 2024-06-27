using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace App.Data.Sql.Core.Interfaces; 

public interface IDbContext : IDisposable {
    string ContextName { get; }
    IModel Model { get; }
    string ConnectionString { get; }
    string Id { get; }
    public DatabaseFacade BaseDatabase { get; }
    ChangeTracker ChangeTracker { get; }
    Task MigrateAsync(CancellationToken                 cancellationToken);
    Task<string[]> GetMigrationsAsync(CancellationToken cancellationToken);
    IDbSet<TEntity> Set<TEntity>() where TEntity : class;
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
}