using App.Data.Sql.Core.Interfaces;

using Microsoft.Extensions.Configuration;

namespace App.Services.RestoreUsers.ConnectionStringProviders; 

public class CFConnectionStringProvider : IConnectionStringProvider {
    public const string CONNECTION_KEY = "CFAppDbContext";

    private readonly IDictionary<string, string> _connections;

    public CFConnectionStringProvider(IConfiguration configuration) {
        _connections = configuration.GetSection("ConnectionStrings")
                                    .GetChildren()
                                    .Where(a => a.Key == CONNECTION_KEY)
                                    .ToDictionary(a => a.Key, a => a.Value);
    }

    public string GetConnection() {
        return _connections.TryGetValue(CONNECTION_KEY, out var connectionString) ? connectionString : string.Empty;
    }

    // "CFAppDbContext": "Host=localhost;Port=5432;Database=CF_Dev;Username=postgres;Password=147654;",
    // "CFAppDbContextOld": "Host=localhost;Port=5432;Database=CF_Dev;Username=postgres;Password=147654;",
    // "PBZZAppDbContext": "Host=localhost;Port=5432;Database=CF_Dev;Username=postgres;Password=147654;",
    // "PBZZAppDbContextOld": "Host=localhost;Port=5432;Database=CF_Dev;Username=postgres;Password=147654;" 
    public string GetConnection(string connectionKey) {
        throw new NotImplementedException();
    }
}