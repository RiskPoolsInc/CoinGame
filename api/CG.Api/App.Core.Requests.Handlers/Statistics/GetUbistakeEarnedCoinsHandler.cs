using App.Core.Enums;
using App.Data.Criterias.Statistics;
using App.Interfaces.Repositories.Games;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Requests.Statistics;

public class GetUbistakeEarnedCoinsHandler : IRequestHandler<GetUbistakeEarnedCoinsRequest, decimal> {
    private readonly IGameRepository _gameRepository;

    public GetUbistakeEarnedCoinsHandler(IGameRepository gameRepository) {
        _gameRepository = gameRepository;
    }

    public async Task<decimal> Handle(GetUbistakeEarnedCoinsRequest request, CancellationToken cancellationToken) {
        var filterByCreatedOn = new GamesByCreatedOn(request.FromDate, request.ToDate);

        var completedGamesQuery = _gameRepository.Where(filterByCreatedOn)
                                                 .Where(a => a.StateId == (int)GameStateTypes.Completed);

        var loseGamesSum = await completedGamesQuery.Where(a => a.ResultId == (int)GameResultTypes.Lose)
                                                    .SumAsync(a => a.RoundSum, cancellationToken);

        //comission from lose games transactions
        var loseGamesDepositsEarnedCoins = loseGamesSum * 0.02m;

        //commission from ubistake payments of lose games
        var ubistakePaymentsSum = loseGamesSum * 0.98m * 0.784m;
        var earnedCoinsByUbistakePayments = ubistakePaymentsSum * 0.02m;

        return Convert.ToInt64(ubistakePaymentsSum - earnedCoinsByUbistakePayments);
    }
}