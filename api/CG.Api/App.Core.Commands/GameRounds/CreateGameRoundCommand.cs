using App.Core.Enums;
using App.Core.ViewModels.Games;

namespace App.Core.Commands.GameRounds;

public class CreateGameRoundCommand : IRequest<GameRoundView> {
    public Guid GameId { get; set; }
    public int GeneratedNumber { get; set; }
    public decimal CurrentGameRoundSum { get; set; }
    public int RoundNumber { get; set; }
    public int Result { get; set; }
    public string GeneratedNumberHash { get; set; }

    public CreateGameRoundCommand() {
    }

    public CreateGameRoundCommand(Guid    gameId, int generatedNumber, string generatedNumberHash, GameRoundResultTypes result,
                                  decimal currentGameRoundSum,
                                  int     roundNumber) {
        GameId = gameId;
        GeneratedNumber = generatedNumber;
        GeneratedNumberHash = generatedNumberHash;
        CurrentGameRoundSum = currentGameRoundSum;
        RoundNumber = roundNumber;
        Result = (int)result;
    }
}