using App.Data.Entities.GameRounds;
using App.Data.Sql.Core.Interfaces;
using App.Interfaces.Repositories.Games;

namespace App.Repositories.Games;

public class GameRoundRepository : AuditableRepository<GameRound>, IGameRoundRepository {
    public GameRoundRepository(IAppDbContext context) : base(context) {
    }
}