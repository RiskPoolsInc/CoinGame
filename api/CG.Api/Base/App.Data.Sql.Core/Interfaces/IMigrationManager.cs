namespace App.Data.Sql.Core.Interfaces; 

public interface IMigrationManager {
    Task MigrateAsync(CancellationToken                        cancellationToken);
    Task<string[]> GetPendingMigrationsAsync(CancellationToken cancellationToken);
}