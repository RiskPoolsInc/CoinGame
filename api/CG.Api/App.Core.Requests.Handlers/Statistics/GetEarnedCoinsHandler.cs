using App.Core.Enums;
using App.Core.Requests.Statistics;
using App.Data.Criterias.Games;
using App.Interfaces.Repositories.Games;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Requests.Handlers.Statistics;

public class GetEarnedCoinsHandler : IRequestHandler<GetEarnedCoinsRequest, decimal>
{
    private readonly IGameRepository _gameRepository;

    public GetEarnedCoinsHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<decimal> Handle(GetEarnedCoinsRequest request, CancellationToken cancellationToken)
    {
        var filterByCreatedOn = new GamesByCreatedOn(request.FromDate, request.ToDate);

        var payedGamesQuery = _gameRepository.Where(filterByCreatedOn)
            .Where(a => a.StateId == (int)GameStateTypes.Payed);

        var gameDepositProfit = await payedGamesQuery.SumAsync(a => a.RoundSum, cancellationToken);

        var gameRewardsProfit = await payedGamesQuery.Where(a => a.ResultId == (int)GameResultTypes.Win)
            .SumAsync(a => a.RewardSum, cancellationToken);

        return Convert.ToInt64(gameDepositProfit * 0.02m + gameRewardsProfit * 0.02m);
    }
}