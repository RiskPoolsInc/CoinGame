using App.Data.Sql;
using App.Data.Sql.Core.Interfaces;
using App.Data.Sql.Core.Providers;
using App.Interfaces.Data.Sql;

using Microsoft.Extensions.Configuration;

namespace App.Services.RestoreUsers.DbContextes;

public abstract class BaseContextFactory  {
    protected abstract IConnectionStringProvider GetConnectionStringProvider(IConfiguration configuration);

    public IAppDbContext Create(IConfiguration config) {
        var provider = new AppDbContextProvider<AppDbContext>(GetConnectionStringProvider(config));
        var factory = new AppDbContextFactory(provider);
        return factory.Create();
    }
}