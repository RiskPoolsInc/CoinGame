using App.Common.Helpers;
using App.Core.Requests.Wallets;
using App.Core.ViewModels.Wallets;
using App.Data.Entities.Wallets;
using App.Interfaces.Handlers.Requests;
using App.Interfaces.Repositories.Wallets;

namespace App.Core.Requests.Handlers.Wallets;

public class GetWalletHandler : IGetWalletHandler {
    private readonly IWalletRepository _walletRepository;

    public GetWalletHandler(IWalletRepository walletRepository) {
        _walletRepository = walletRepository;
    }

    public Task<WalletView> Handle(GetWalletRequest request, CancellationToken cancellationToken) {
        var walletView = _walletRepository.Get(request.Id).SingleAsync<Wallet, WalletView>(cancellationToken);
        return walletView;
    }
}