using System.Linq.Expressions;
using App.Core.Enums;
using App.Data.Criterias.Core;
using App.Data.Entities.Games;

namespace App.Data.Criterias.Games;

public class NotPayedLoseGamesFilter : ACriteria<Game> {
    public override Expression<Func<Game, bool>> Build() {
        return a => a.StateId == (int)GameStateTypes.Completed &&
            !a.TransactionServices.Any(s => s.GameId == a.Id) 
            && a.ResultId == (int) GameResultTypes.Lose;
    }
}