using System.Linq.Expressions;

using App.Core.Enums;
using App.Data.Criterias.Core;
using App.Data.Entities.Games;

namespace App.Data.Criterias.Statistics;

public class CompletedWinGamesWithoutTransactionsFilter : ACriteria<Game> {
    public override Expression<Func<Game, bool>> Build() {
        return a => a.StateId == (int)GameStateTypes.Completed && !a.TransactionUserRewards.Any(s => s.GameId == a.Id);
    }
}