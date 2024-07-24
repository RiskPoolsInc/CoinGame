using App.Core.ViewModels.Games;

namespace App.Core.Requests.Games;

public class GetGameRequest : IRequest<GameView> {
    public Guid GameId { get; set; }

    public GetGameRequest() {
        
    }

    public GetGameRequest(Guid gameId) {
        GameId = gameId;
    }
}