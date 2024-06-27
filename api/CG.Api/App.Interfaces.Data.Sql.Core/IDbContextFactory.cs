using App.Data.Sql.Core.Interfaces;

namespace App.Interfaces.Data.Sql.Core;

public interface IDbContextFactory<out T> : IDbContextFactory where T : IDbContext {
    new T Create(params object[] args);
}

public interface IDbContextFactory {
    IDbContext Create(params object[] args);
}