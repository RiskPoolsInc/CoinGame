using App.Data.Sql.Core.Interfaces;
using App.Data.Sql.DbContext;
using App.Interfaces.Data.Sql;
using App.Interfaces.Data.Sql.Core;

namespace App.Data.Sql;

public class AppDbContextFactory : DbContextFactory<AppDbContext>, IAppDbContextFactory {
    public AppDbContextFactory(IAppDbContextProvider<AppDbContext> provider) : base(provider) {
    }

    IAppDbContext IDbContextFactory<IAppDbContext>.Create(params object[] args) {
        return Create();
    }

    public override AppDbContext Create(params object[] args) {
        return new AppDbContext(GetOptions());
    }
}