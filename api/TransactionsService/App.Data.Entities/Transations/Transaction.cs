using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.TransactionReceivers;
using App.Data.Entities.Wallets;

namespace App.Data.Entities.Transactions;

public abstract class Transaction : AuditableEntity {
    public Guid? WalletFromId { get; set; }
    public virtual Wallet WalletFrom { get; set; }
    public string WalletHashFrom { get; set; }

    public string Hash { get; set; }

    public decimal Sum { get; set; }
    public decimal Fee { get; set; }

    public virtual int TypeId { get; set; } //[Base, Refund]
    public virtual TransactionType Type { get; set; }

    public virtual int StateId { get; set; } //[NotCreated, Completed, InProgress]
    public virtual TransactionStateType State { get; set; }

    public bool ExistInBlockChain { get; set; }

    public virtual ICollection<TransactionReceiver> Receivers { get; set; }
}