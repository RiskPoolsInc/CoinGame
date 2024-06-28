using App.Common.Helpers;
using App.Core.Requests.Games;
using App.Core.ViewModels.Games;
using App.Data.Entities.Games;
using App.Interfaces.Repositories.Games;

using MediatR;

namespace App.Core.Requests.Handlers.Games;

public class GetGamesHandler : IRequestHandler<GetGamesRequest, GameView[]> {
    private readonly IGameRepository _gameRepository;

    public GetGamesHandler(IGameRepository gameRepository) {
        _gameRepository = gameRepository;
    }

    public async Task<GameView[]> Handle(GetGamesRequest request, CancellationToken cancellationToken) {
        var games = await _gameRepository.Where(a => a.WalletId == request.WalletId).ToArrayAsync<Game, GameView>(cancellationToken);
        return games;
    }
}