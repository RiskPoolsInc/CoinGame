using App.Core.Enums;
using App.Core.Requests.Statistics;
using App.Data.Criterias.Games;
using App.Interfaces.Repositories.Games;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Requests.Handlers.Statistics;

public class GetGamesRewardsSumHandler : IRequestHandler<GetGamesRewardsSumRequest, decimal> {
    private readonly IGameRepository _gameRepository;

    public GetGamesRewardsSumHandler(IGameRepository gameRepository) {
        _gameRepository = gameRepository;
    }

    public async Task<decimal> Handle(GetGamesRewardsSumRequest request, CancellationToken cancellationToken) {
        var gameRewards = await _gameRepository.Where(new GamesByCreatedOn(request.FromDate, request.ToDate))
                                               .Where(a => a.StateId == (int)GameStateTypes.Payed &&
                                                    a.ResultId == (int)GameResultTypes.Win)
                                               .SumAsync(s => s.RewardSum, cancellationToken);
        return gameRewards;
    }
}