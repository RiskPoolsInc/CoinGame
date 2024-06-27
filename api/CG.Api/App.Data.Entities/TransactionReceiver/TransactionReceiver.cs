using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;

namespace App.Data.Entities.TransactionReceiver;

public class TransactionReceiver: AuditableEntity {
    public Guid TransactionId { get; set; }
    public string WalletHash { get; set; }
    public decimal Sum { get; set; }

    public int TypeId { get; set; }
    public virtual TransactionReceiverType Type { get; set; }
}