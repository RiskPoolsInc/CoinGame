using App.Core.ViewModels.Games;

namespace App.Core.Requests.Games;

public class GetGamesRequest : IRequest<GameView[]> {
    public Guid WalletId { get; set; }
}