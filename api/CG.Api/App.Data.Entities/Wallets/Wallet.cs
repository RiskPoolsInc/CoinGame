using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.Games;
using App.Data.Entities.Transactions;

namespace App.Data.Entities.Wallets;

public class Wallet : ArchivableEntity {
    public string Hash { get; set; }

    public int TypeId { get; set; }              // [Game, Service, Commission]
    public virtual WalletType Type { get; set; } // [Game, Service, Commission]
    public string? PrivateKey { get; set; }
    public bool IsBlocked { get; set; }
    public Guid? RefundId { get; set; }
    public Guid ImportedWalletId { get; set; }
    public virtual TransactionUserRefund Refund { get; set; }

    public virtual ICollection<Game> Games { get; set; }
}