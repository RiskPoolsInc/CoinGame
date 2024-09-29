using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.ServiceProfiles;
using App.Data.Entities.Transactions;
using App.Data.Entities.Wallets;

namespace App.Data.Entities.TransactionLogs;

public class LogEntity : BaseEntity {
    public int TypeId { get; set; }
    public virtual LogEntityType Type { get; set; }
    public string WalletServiceRequestBody { get; set; }
    public string? WalletServiceResponceBody { get; set; }
    public string? Error { get; set; }
    public string WalletServiceRequestReceiver { get; set; }
    public Guid SenderId { get; set; }
    public virtual ServiceProfile Sender { get; set; }
    public Guid? WalletId { get; set; }
    public virtual Wallet Wallet { get; set; }
    public Guid? TransactionId { get; set; }
    public virtual BaseTransaction Transaction { get; set; }
}