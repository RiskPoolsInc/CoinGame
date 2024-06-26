using App.Data.Entities.Pbz.Wallets;
using App.Interfaces.Repositories.Pbz.Wallets;

namespace App.Repozitories.Pbz.Wallets {

public class WalletRepository: ArchivableRepository<Wallet>, IWalletRepository
{
    public WalletRepository(IPbzDbContext context) : base(context)
    {
    }
}
}