using App.Core.Configurations;
using App.Interfaces.ExternalServices;
using App.Interfaces.Repositories;
using App.Interfaces.Repositories.Wallets;

namespace App.Infrastructure.VirtualWalletService;

public class VirtualWallerService : IWalletService {
    private readonly IWalletRepository _walletRepository;
    private readonly WalletSettings _walletSettings;

    public VirtualWallerService(WalletSettings walletSettings, IWalletRepository walletRepository) {
        _walletSettings = walletSettings;
        _walletRepository = walletRepository;
    }

    public Task<string> CreateWallet(string name, CancellationToken cancellationToken) {
        return Task.FromResult(_walletSettings.Address);
    }

    public Task<decimal> GetBalance(string address, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}