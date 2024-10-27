using App.Core.Requests.Statistics;
using App.Data.Criterias.Games;
using App.Interfaces.Repositories.Games;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Requests.Handlers.Statistics;

public class GetGamesRatesSumHandler : IRequestHandler<GetGamesRatesSumRequest, decimal> {
    private readonly IGameRepository _gameRepository;

    public GetGamesRatesSumHandler(IGameRepository gameRepository) {
        _gameRepository = gameRepository;
    }

    public async Task<decimal> Handle(GetGamesRatesSumRequest request, CancellationToken cancellationToken) {
        var gamesRates = await _gameRepository.Where(new GamesByCreatedOn(request.FromDate, request.ToDate))
                                              .SumAsync(s => s.RoundSum, cancellationToken);
        return gamesRates;
    }
}