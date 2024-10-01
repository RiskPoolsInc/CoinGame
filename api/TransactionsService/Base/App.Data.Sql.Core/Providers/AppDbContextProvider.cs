using App.Data.Sql.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Sql.Core.Providers;

public class AppDbContextProvider<TContext> : IAppDbContextProvider<TContext> where TContext : BaseDbContext
{
    private readonly DbContextOptionsBuilder<TContext> _builder;
    private readonly IConnectionStringProvider _connectionStringProvider;
    private readonly Type _contextType;

    public AppDbContextProvider(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
        _contextType = typeof(TContext);
        _builder = new DbContextOptionsBuilder<TContext>();
    }

    public DbContextOptions<TContext> GetOptions()
    {
        var contextName = typeof(TContext);
        var connectionString = _connectionStringProvider.GetConnection(contextName.Name);

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException("DbConnectionString was not found");

        _builder.UseNpgsql(connectionString, a =>
        {
            a.CommandTimeout((int) TimeSpan.FromMinutes(5).TotalSeconds);
            a.EnableRetryOnFailure(3);
            a.MigrationsAssembly(_contextType.Assembly.FullName);
            a.MigrationsHistoryTable(string.Format("__{0}Migrations", _contextType.Name));
            a.UseNetTopologySuite();
        });

        return _builder.Options;
    }
}