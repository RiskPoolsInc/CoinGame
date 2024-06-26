using App.Data.Sql.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace App.Interfaces.Data.Sql.Pbz;

public interface IPbzDbContext : IDbContext
{
    public void AddProvider(ILoggerProvider provider);
}