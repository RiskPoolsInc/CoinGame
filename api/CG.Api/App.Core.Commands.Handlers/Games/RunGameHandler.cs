using App.Core.Commands.Games;
using App.Core.ViewModels.Games;

namespace App.Core.Commands.Handlers.Games;

public class RunGameHandler: IRequestHandler<RunGameCommand, GameView> {
    public Task<GameView> Handle(RunGameCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}