using System.Diagnostics;

using App.Data.Sql.Core.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace App.Data.Sql.Core;

[DebuggerDisplay("{ContextName}:{Id}")]
public abstract class BaseDbContext : DbContext, IDbContext, IDbFacadeProvider {
    private bool _isDisposed;

    protected BaseDbContext(DbContextOptions options) : base(options) {
        var optionsExtensions = options.Extensions.First() as NpgsqlOptionsExtension;

        if (optionsExtensions != null)
            ConnectionString = optionsExtensions.ConnectionString;

        ConnectionString ??= optionsExtensions?.ConnectionString;
        Id = Guid.NewGuid().ToString();
    }
    public void AddProvider(ILoggerProvider provider) {
        this.GetService<ILoggerFactory>().AddProvider(provider);
    }
    public string ContextName => GetType().Name;

    public string ConnectionString { get; }
    public string Id { get; }
    public DatabaseFacade BaseDatabase => Database;

    IDbSet<TEntity> IDbContext.Set<TEntity>() where TEntity : class {
        var query = new DbQuery<TEntity>(Set<TEntity>());
        return query;
    }

    public override void Dispose() {
        if (_isDisposed)
            return;
        base.Dispose();
        _isDisposed = true;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) {
        var result = await base.SaveChangesAsync(cancellationToken);
        DetachAll();
        return result;
    }

    public override int SaveChanges() {
        var result = base.SaveChanges();
        DetachAll();
        return result;
    }

    public Task MigrateAsync(CancellationToken cancellationToken) {
        return Database.MigrateAsync(cancellationToken);
    }

    public async Task<string[]> GetMigrationsAsync(CancellationToken cancellationToken) {
        var migrations = await Database.GetPendingMigrationsAsync(cancellationToken);
        return migrations.ToArray();
    }

    private void DetachAll() {
        foreach (var entry in ChangeTracker.Entries())
            if (entry.Entity != null)
                entry.State = EntityState.Detached;
    }
}