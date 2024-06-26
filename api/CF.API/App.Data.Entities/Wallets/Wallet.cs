using App.Data.Entities.Core;
using App.Data.Entities.Payments;
using App.Data.Entities.UserProfiles;

namespace App.Data.Entities.Wallets;

public class Wallet : ArchivableEntity {
    public string Hash { get; set; }
    public int TypeId { get; set; }// [Game, Service, Commission]
    public string PrivateKey { get; set; }
    public bool IsBlocked { get; set; }
    public Guid? RefundId { get; set; }
    public virtual TransactionUserRefund Refund { get; set; }
}