using System.Data.Common;

using App.Data.Sql.Core.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace App.Data.Sql.Core;

public class DbConnectionAdapter<TContext> : IDbConnectionAdapter<TContext> where TContext : IDbContext {
    private readonly IDbFacadeProvider _provider;

    public DbConnectionAdapter(TContext context) {
        _provider = context as IDbFacadeProvider;
    }

    public DbConnection GetConnection() {
        return _provider.Database.GetDbConnection();
    }
}