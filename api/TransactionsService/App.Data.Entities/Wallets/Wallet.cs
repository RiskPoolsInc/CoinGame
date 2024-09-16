using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.ServiceProfiles;
using App.Data.Entities.Transactions;

namespace App.Data.Entities.Wallets;

public class Wallet : ArchivableEntity {
    public Guid CreatedById { get; set; }
    public virtual ServiceProfile CreatedBy { get; set; }
    public string Address { get; set; }
    public int TypeId { get; set; }              // [Generated, Imported]
    public virtual WalletType Type { get; set; } // [Generated, Imported]
    public string PrivateKey { get; set; }
    public bool IsEncrypted { get; set; }

    public virtual ICollection<BaseTransaction> Transactions { get; set; }
}