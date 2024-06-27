using App.Core.Commands.GameRounds;
using App.Core.ViewModels.Games;

namespace App.Core.Commands.Handlers.GameRounds;

public class CreateGameRoundHandler : IRequestHandler<CreateGameRoundCommand, GameRoundView> {
    public Task<GameRoundView> Handle(CreateGameRoundCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}