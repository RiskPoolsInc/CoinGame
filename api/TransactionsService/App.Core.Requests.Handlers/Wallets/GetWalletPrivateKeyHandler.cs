using App.Common.Helpers;
using App.Core.Requests.Wallets;
using App.Core.ViewModels.Wallets;
using App.Data.Entities.Wallets;
using App.Interfaces.Handlers.RequestHandlers;
using App.Interfaces.Repositories.Wallets;
using App.Services.WalletService;

namespace App.Core.Requests.Handlers.Wallets;

public class GetWalletPrivateKeyHandler : IGetWalletPrivateKeyHandler {
    private readonly IWalletRepository _walletRepository;
    private readonly IWalletService _walletService;

    public GetWalletPrivateKeyHandler(IWalletRepository walletRepository, IWalletService walletService) {
        _walletRepository = walletRepository;
        _walletService = walletService;
    }

    public async Task<WalletPrivateKeyView> Handle(GetWalletPrivateKeyRequest request, CancellationToken cancellationToken) {
        var privateKeyView = await _walletRepository.Get(request.WalletId).SingleAsync<Wallet, WalletPrivateKeyView>(cancellationToken);
        var privateKey = privateKeyView.PrivateKey;
        privateKeyView.PrivateKey = _walletService.DecryptPrivateKey(privateKey, privateKeyView.Id);
        return privateKeyView;
    }
}