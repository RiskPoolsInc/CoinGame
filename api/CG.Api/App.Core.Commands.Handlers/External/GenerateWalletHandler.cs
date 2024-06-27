using App.Core.Commands.Wallets;
using App.Core.ViewModels.External;
using App.Services.WalletService;

namespace App.Core.Commands.Handlers.Wallets;

public class GenerateWalletHandler : IRequestHandler<GenerateWalletCommand, GeneratedWalletView> {
    private readonly WalletService _walletService;

    public GenerateWalletHandler(WalletService walletService) {
        _walletService = walletService;
    }

    public async Task<GeneratedWalletView> Handle(GenerateWalletCommand request, CancellationToken cancellationToken) {
        var generatedWallet = await _walletService.GenerateWallet();
        return generatedWallet;
    }
}