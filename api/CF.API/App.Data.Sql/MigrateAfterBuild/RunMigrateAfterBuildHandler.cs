using App.Interfaces.Data.Sql;
using App.Interfaces.Data.Sql.Core;
using MediatR;

namespace App.Data.Sql.MigrateAfterBuild;

public class RunMigrateAfterBuildHandler : IRequestHandler<RunMigrateAfterBuild, string[]>
{
    private readonly IDbContextFactory _dbContextFactory;

    public RunMigrateAfterBuildHandler(IDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public Task<string[]> Handle(RunMigrateAfterBuild request, CancellationToken cancellationToken)
    {
        return TryMigrateDatabase(request, _dbContextFactory, cancellationToken);
    }

    private async Task<string[]> TryMigrateDatabase(RunMigrateAfterBuild request,
        IDbContextFactory contextFactory,
        CancellationToken cancellationToken
    )
    {
        var type = contextFactory.GetType();
        var name = type.Name;
        if (name.Equals("PbzDbContextFactory"))
            return Array.Empty<string>();

        var maxAttempts = 5;
        Exception? lastException = null;

        while (--maxAttempts >= 0)
        {
            try
            {
                using var context = contextFactory.Create();
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