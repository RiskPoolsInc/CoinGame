using App.Data.Criterias.Statistics;
using App.Interfaces.Repositories.Games;

using MediatR;

namespace App.Core.Requests.Statistics;

public class GetCountGamesHandler : IRequestHandler<GetCountGamesRequest, int> {
    private readonly IGameRepository _gameRepository;

    public GetCountGamesHandler(IGameRepository gameRepository) {
        _gameRepository = gameRepository;
    }

    public async Task<int> Handle(GetCountGamesRequest request, CancellationToken cancellationToken) {
        var count = await _gameRepository.CountAsync(new GamesByCreatedOn(request.FromDate, request.ToDate), cancellationToken);
        return count;
    }
}