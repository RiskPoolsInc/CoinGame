using App.Data.Sql.Core.Interfaces;

using Microsoft.Extensions.Logging;

namespace App.Interfaces.Data.Sql;

public interface IAppDbContext : IDbContext {
    public void AddProvider(ILoggerProvider provider);
}