using App.Core.ViewModels.Games;

namespace App.Core.Requests.Games;

public class GetGamesRequest : IRequest<GameView[]> {
    public string WalletHash { get; set; }
    public Guid WalletId { get; set; }
}