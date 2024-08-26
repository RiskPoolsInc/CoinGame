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

        var winGameSumCoins = await completedGamesQuery.Where(a => a.ResultId == (int)GameResultTypes.Win)
                                                       .SumAsync(a => a.RoundSum, cancellationToken);

        //comission from win games transactions
        var winGamesDepositsEarnedCoins = winGameSumCoins * 0.02m;

        var gameRewardsSumCoins = await completedGamesQuery.Where(a => a.ResultId == (int)GameResultTypes.Win)
                                                           .SumAsync(a => a.RewardSum, cancellationToken);

        //comission from reward games transactions
        var gameRewardsEarnedCoins = gameRewardsSumCoins * 0.02m;

        var loseGamesSum = await completedGamesQuery.Where(a => a.ResultId == (int)GameResultTypes.Lose)
                                                    .SumAsync(a => a.RoundSum, cancellationToken);

        //comission from lose games transactions
        var loseGamesDepositsEarnedCoins = loseGamesSum * 0.02m;

        return Convert.ToInt64(loseGamesDepositsEarnedCoins +
            winGamesDepositsEarnedCoins +
            gameRewardsEarnedCoins);
    }
}