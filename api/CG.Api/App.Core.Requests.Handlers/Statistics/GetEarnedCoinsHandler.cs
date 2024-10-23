using App.Core.Enums;
using App.Data.Criterias.Statistics;
using App.Interfaces.Repositories.Games;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Requests.Statistics;

public class GetEarnedCoinsHandler : IRequestHandler<GetEarnedCoinsRequest, decimal> {
    private readonly IGameRepository _gameRepository;

    public GetEarnedCoinsHandler(IGameRepository gameRepository) {
        _gameRepository = gameRepository;
    }

    public async Task<decimal> Handle(GetEarnedCoinsRequest request, CancellationToken cancellationToken) {
        var filterByCreatedOn = new GamesByCreatedOn(request.FromDate, request.ToDate);

        var completedGamesQuery = _gameRepository.Where(filterByCreatedOn)
                                                 .Where(a => a.StateId == (int)GameStateTypes.Completed);
        
        var gameDepositProfit = await completedGamesQuery.SumAsync(a => a.RoundSum, cancellationToken);

        var gameRewardsProdit = await completedGamesQuery.Where(a => a.ResultId == (int)GameResultTypes.Win)
                                                         .SumAsync(a => a.RewardSum, cancellationToken);

        return Convert.ToInt64(gameDepositProfit * 0.02m + gameRewardsProdit * 0.02m);
    }
}