using App.Data.Sql.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace App.Data.Sql.Core.Providers;

public class ConnectionStringProvider : IConnectionStringProvider
{
    private const string CONNECTION_KEY = "AppDbContext";

    private IDictionary<string, string> _connections;
    private readonly IEnumerable<IConfigurationSection> _connectionStrings;

    public ConnectionStringProvider(IConfiguration configuration)
    {
        _connectionStrings = configuration.GetSection("ConnectionStrings")
            .GetChildren();

        _connections = configuration.GetSection("ConnectionStrings")
            .GetChildren()
            .Where(a => a.Key == CONNECTION_KEY)
            .ToDictionary(a => a.Key, a => a.Value);
    }

    public string GetConnection(string connectionKey = CONNECTION_KEY)
    {
        _connections = GetDictionaryConnectionString(connectionKey);
        return _connections.TryGetValue(connectionKey, out var connectionString) ? connectionString : string.Empty;
    }

    private IDictionary<string, string> GetDictionaryConnectionString(string connectionKey)
    {
        return _connectionStrings
            .Where(a => a.Key == connectionKey)
            .ToDictionary(a => a.Key, a => a.Value);
    }
}