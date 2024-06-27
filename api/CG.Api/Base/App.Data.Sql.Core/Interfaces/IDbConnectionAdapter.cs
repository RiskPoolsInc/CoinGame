using System.Data.Common;

namespace App.Data.Sql.Core.Interfaces; 

public interface IDbConnectionAdapter {
    DbConnection GetConnection();
}

public interface IDbConnectionAdapter<out TContext> : IDbConnectionAdapter
    where TContext : IDbContext {
}