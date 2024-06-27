using App.Data.Sql.Core.Interfaces;

namespace App.Data.Sql.Core.Migrations;

public class MigrationManager : IMigrationManager {
    private readonly IDbContext _context;

    public MigrationManager(IDbContext context) {
        _context = context;
    }

    public async Task MigrateAsync(CancellationToken cancellationToken) {
        await _context.MigrateAsync(cancellationToken);
    }

    public Task<string[]> GetPendingMigrationsAsync(CancellationToken cancellationToken) {
        return _context.GetMigrationsAsync(cancellationToken);
    }
}