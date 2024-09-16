using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.ServiceProfiles;
using App.Data.Entities.TransactionReceivers;
using App.Data.Entities.Wallets;

namespace App.Data.Entities.Transactions;

public abstract class BaseTransaction : AuditableEntity {
    public Guid CreatedById { get; set; }
    public virtual ServiceProfile CreatedBy { get; set; }

    public Guid? WalletId { get; set; }
    public virtual Wallet Wallet { get; set; }
    public string Address { get; set; }

    public string? Hash { get; set; }

    public decimal Sum { get; set; }
    public decimal Fee { get; set; }

    public virtual int TypeId { get; set; } //[Base, Refund]
    public virtual TransactionType Type { get; set; }

    public virtual int StateId { get; set; } //[NotCreated, Completed, InProgress]
    public virtual TransactionStateType State { get; set; }

    public bool ExistInBlockChain { get; set; }

    public string Error { get; set; }

    public virtual ICollection<TransactionReceiver> Receivers { get; set; }
}