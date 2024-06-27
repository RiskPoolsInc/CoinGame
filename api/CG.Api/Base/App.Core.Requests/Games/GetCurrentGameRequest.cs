using App.Core.ViewModels.Games;

namespace App.Core.Requests.Games;

public class GetCurrentGameRequest : IRequest<GameView> {
    public Guid WalletId { get; set; }
    public string WalletHash { get; set; }
}