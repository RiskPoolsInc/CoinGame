using App.Core.Enums;
using App.Core.Requests.Statistics;
using App.Data.Criterias.Games;
using App.Interfaces.Repositories.Games;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Requests.Handlers.Statistics;

public class GetUbistakeEarnedCoinsHandler : IRequestHandler<GetUbistakeEarnedCoinsRequest, decimal> {
    private readonly IGameRepository _gameRepository;

    public GetUbistakeEarnedCoinsHandler(IGameRepository gameRepository) {
        _gameRepository = gameRepository;
    }

    public async Task<decimal> Handle(GetUbistakeEarnedCoinsRequest request, CancellationToken cancellationToken) {
        var filterByCreatedOn = new GamesByCreatedOn(request.FromDate, request.ToDate);

        var payedGamesQuery = _gameRepository.Where(filterByCreatedOn)
                                                 .Where(a => a.StateId == (int)GameStateTypes.Payed);

        var loseGamesSum = await payedGamesQuery.Where(a => a.ResultId == (int)GameResultTypes.Lose)
                                                    .SumAsync(a => a.RoundSum, cancellationToken);
        
        //commission from ubistake payments of lose games
        var ubistakePaymentsSum = loseGamesSum * 0.784m;

        return Convert.ToInt64(ubistakePaymentsSum);
    }
}