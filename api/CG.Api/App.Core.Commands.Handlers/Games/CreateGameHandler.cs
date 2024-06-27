using App.Core.Commands.Games;
using App.Core.ViewModels.Games;

namespace App.Core.Commands.Handlers.Games;

public class CreateGameHandler : IRequestHandler<CreateGameCommand, GameView> {
    public Task<GameView> Handle(CreateGameCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}