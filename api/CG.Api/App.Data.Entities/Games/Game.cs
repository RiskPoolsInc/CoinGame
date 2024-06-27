using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.GameRounds;
using App.Data.Entities.Payments;
using App.Data.Entities.Wallets;

namespace App.Data.Entities.Games;

public class Game : AuditableEntity {
    public Guid WalletId { get; set; }
    public virtual Wallet Wallet { get; set; }

    public int? StateId { get; set; } //[Created, InProgress, Complete]
    public virtual GameState State { get; set; }

    public int? ResultId { get; set; } //[Undefined, Win, Lose]
    public virtual GameResult Result { get; set; }

    public int RoundQuantity { get; set; }
    public decimal RoundSum { get; set; }

    public virtual ICollection<GameRound> GameRounds { get; set; }
}