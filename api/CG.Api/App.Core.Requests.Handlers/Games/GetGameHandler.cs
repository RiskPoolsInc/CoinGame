using App.Common.Helpers;
using App.Core.Requests.Games;
using App.Core.ViewModels.Games;
using App.Data.Entities.Games;
using App.Interfaces.Repositories.Games;

using MediatR;

namespace App.Core.Requests.Handlers.Games;

public class GetGameHandler : IRequestHandler<GetGameRequest, GameView> {
    private readonly IGameRepository _gameRepository;

    public GetGameHandler(IGameRepository gameRepository) {
        _gameRepository = gameRepository;
    }

    public async Task<GameView> Handle(GetGameRequest request, CancellationToken cancellationToken) {
        var game = await _gameRepository.Get(request.GameId).SingleAsync<Game, GameView>(cancellationToken);
        return game;
    }
}