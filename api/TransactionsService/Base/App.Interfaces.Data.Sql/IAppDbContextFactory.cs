using App.Interfaces.Data.Sql.Core;

namespace App.Interfaces.Data.Sql;

public interface IAppDbContextFactory : IDbContextFactory<IAppDbContext> {
}