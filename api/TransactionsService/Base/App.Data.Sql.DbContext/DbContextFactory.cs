using App.Data.Sql.Core;
using App.Data.Sql.Core.Interfaces;
using App.Interfaces.Data.Sql;
using App.Interfaces.Data.Sql.Core;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Sql.DbContext; 

public abstract class DbContextFactory<T> : IDbContextFactory where T : BaseDbContext {
    private readonly IAppDbContextProvider<T> _contextProvider;

    protected DbContextFactory(IAppDbContextProvider<T> contextProvider) {
        _contextProvider = contextProvider;
    }

    IDbContext IDbContextFactory.Create(params object[] args) {
        return Create();
    }

    public abstract T Create(params object[] args);

    protected DbContextOptions<T> GetOptions() {
        var options = _contextProvider.GetOptions();
        return options;
    }
}