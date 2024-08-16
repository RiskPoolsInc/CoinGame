using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.Games;
using App.Data.Entities.UserProfiles;
using App.Data.Entities.Wallets;

namespace App.Data.Entities.Transactions;

public abstract class Transaction : AuditableEntity {
    public Guid? GameId { get; set; }
    public virtual Game Game { get; set; }

    public Guid? WalletFromId { get; set; }
    public virtual Wallet WalletFrom { get; set; }
    public string WalletHashFrom { get; set; }

    public string? TransactionHash { get; set; }

    public decimal Sum { get; set; }
    public decimal Fee { get; set; }

    public virtual int TypeId { get; set; } //[Deposit, GameReward, Refund, Service]
    public virtual TransactionType Type { get; set; }

    public virtual int StateId { get; set; } //[NotCreated, Completed, InProgress]
    public virtual TransactionStateType State { get; set; }

    public bool ExistInBlockChain { get; set; }
    public bool SkipTransaction { get; set; }
    public int SkipTransactionReasonId { get; set; }
    public string? Error { get; set; }
    public bool ErrorTransaction { get; set; }
}