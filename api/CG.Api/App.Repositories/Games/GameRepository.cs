using App.Data.Entities.Games;
using App.Data.Sql.Core.Interfaces;
using App.Interfaces.Repositories.Games;

namespace App.Repositories.Games;

public class GameRepository : AuditableRepository<Game>, IGameRepository {
    public GameRepository(IAppDbContext context) : base(context) {
    }
}