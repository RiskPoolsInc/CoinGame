using App.Data.Entities.Games;

namespace App.Data.Criterias.Games;

public class GamesByCreatedOn : ACriteriaPeriod<Game> {
    public GamesByCreatedOn(DateTime? from = null, DateTime? to = null) : base(from, to) {
    }
}