using App.Core.Commands.Database;
using App.Interfaces.Data.Sql;
using App.Interfaces.Data.Sql.Core;

namespace App.Core.Commands.Handlers.Database {

public class MigrateDatabaseHandler : IRequestHandler<MigrateDatabaseCommand, string[]>
{
    private readonly IDbContextFactory _dbContextFactory;

    public MigrateDatabaseHandler(IDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public Task<string[]> Handle(MigrateDatabaseCommand request, CancellationToken cancellationToken)
    {
        return TryMigrateDatabase(request, _dbContextFactory, cancellationToken);
    }

    private async Task<string[]> TryMigrateDatabase(MigrateDatabaseCommand request, IDbContextFactory contextFactory, CancellationToken cancellationToken)
    {
        var maxAttempts = 5;
        Exception? lastException = null;
        while (--maxAttempts >= 0)
        {
            try
            {
                using var context = contextFactory.Create();
                var s = context.ConnectionString;
                await context.MigrateAsync(cancellationToken);
                return await context.GetMigrationsAsync(cancellationToken);
            }
            catch (Exception? ex)
            {
                lastException = ex;
            }

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        }

        throw lastException ?? new Exception("Something went wrong");
    }
}
}