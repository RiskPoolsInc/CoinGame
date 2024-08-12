using System.Linq.Expressions;

using App.Data.Criterias.Core;
using App.Data.Criterias.Core.Helpers;
using App.Data.Entities.Games;

namespace App.Data.Criterias.Statistics;

public class GamesByCreatedOn : ACriteriaPeriod<Game> {
    public GamesByCreatedOn(DateTime? from = null, DateTime? to = null) : base(from, to) {
    }
}