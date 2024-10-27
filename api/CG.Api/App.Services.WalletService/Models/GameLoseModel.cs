namespace App.Services.WalletService.Models;

public class GameLoseModel
{
    public decimal Bet { get; set; }
    public Guid GameId { get; set; }

    public GameLoseModel()
    {
    }

    public GameLoseModel(Guid gameId, decimal bet)
    {
        GameId = gameId;
        Bet = bet;
    }
}