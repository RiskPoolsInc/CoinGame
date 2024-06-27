using App.Core.ViewModels.Games;

namespace App.Core.Commands.Games;

public class CreateGameCommand: IRequest<GameView> {
    public Guid WalletId { get; set; }
}