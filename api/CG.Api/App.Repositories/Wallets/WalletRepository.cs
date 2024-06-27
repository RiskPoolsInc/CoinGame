using App.Data.Entities.Wallets;
using App.Interfaces.Repositories;
using App.Interfaces.Repositories.Wallets;

namespace App.Repositories.Wallets;

public class WalletRepository : ArchivableRepository<Wallet>, IWalletRepository {
    public WalletRepository(IAppDbContext context) : base(context) {
    }
}