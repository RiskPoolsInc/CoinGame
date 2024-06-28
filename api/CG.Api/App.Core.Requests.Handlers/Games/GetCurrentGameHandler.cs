using App.Common.Helpers;
using App.Core.Enums;
using App.Core.Requests.Games;
using App.Core.ViewModels.Games;
using App.Data.Entities.Games;
using App.Interfaces.Repositories.Games;

using MediatR;

namespace App.Core.Requests.Handlers.Games;

public class GetCurrentGameHandler : IRequestHandler<GetCurrentGameRequest, GameView> {
    private readonly IGameRepository _gameRepository;

    public GetCurrentGameHandler(IGameRepository gameRepository) {
        _gameRepository = gameRepository;
    }

    public async Task<GameView> Handle(GetCurrentGameRequest request, CancellationToken cancellationToken) {
        var game = await _gameRepository.Where(a => a.Wallet.Id == request.WalletId && a.StateId == (int)GameStateTypes.InProgress)
                                        .SingleAsync<Game, GameView>(cancellationToken);
        return game;
    }
}