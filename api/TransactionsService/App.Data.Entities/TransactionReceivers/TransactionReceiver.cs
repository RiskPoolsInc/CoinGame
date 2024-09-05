using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.Transactions;

namespace App.Data.Entities.TransactionReceivers;

public class TransactionReceiver : AuditableEntity {
    public Guid TransactionId { get; set; }
    public virtual Transaction Transaction { get; set; }
    public string FromAddress { get; set; }
    public string Address { get; set; }
    public decimal Sum { get; set; }

    public int TypeId { get; set; }
    public virtual TransactionReceiverType Type { get; set; }
}