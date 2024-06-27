using App.Core.ViewModels.Games;

namespace App.Core.Commands.GameRounds;

public class CreateGameRoundCommand : IRequest<GameRoundView> {
    public Guid GameId { get; set; }
}