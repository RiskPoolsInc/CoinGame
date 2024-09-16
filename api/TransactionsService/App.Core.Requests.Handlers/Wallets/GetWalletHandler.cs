using App.Common.Helpers;
using App.Core.Requests.Wallets;
using App.Core.ViewModels.Wallets;
using App.Data.Entities.Wallets;
using App.Interfaces.Handlers.Requests;
using App.Interfaces.Repositories.Wallets;
using App.Services.WalletService;

namespace App.Core.Requests.Handlers.Wallets;

public class GetWalletHandler : IGetWalletHandler {
    private readonly IWalletRepository _walletRepository;
    private readonly IWalletService _walletService;

    public GetWalletHandler(IWalletRepository walletRepository, IWalletService walletService) {
        _walletRepository = walletRepository;
        _walletService = walletService;
    }

    public async Task<WalletView> Handle(GetWalletRequest request, CancellationToken cancellationToken) {
        var wallet = await _walletRepository.Get(request.Id).SingleAsync<Wallet, WalletView>(cancellationToken);
        return wallet;
    }
}