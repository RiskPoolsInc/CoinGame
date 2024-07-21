using App.Common.Helpers;
using App.Core.Requests.Wallets;
using App.Core.ViewModels.Wallets;
using App.Data.Entities.Wallets;
using App.Interfaces.Repositories.Wallets;
using App.Services.WalletService;

using MediatR;

namespace App.Core.Requests.Handlers.Wallets;

public class GetWalletBalanceHandler : IRequestHandler<GetWalletBalanceRequest, WalletBalanceView> {
    private readonly IWalletRepository _walletRepository;
    private readonly IWalletService _walletService;

    public GetWalletBalanceHandler(IWalletRepository walletRepository, IWalletService walletService) {
        _walletRepository = walletRepository;
        _walletService = walletService;
    }

    public async Task<WalletBalanceView> Handle(GetWalletBalanceRequest request, CancellationToken cancellationToken) {
        var wallet = await _walletRepository.Get(request.WalletId).SingleAsync<Wallet, WalletBalanceView>(cancellationToken);
        wallet.Balance = (await _walletService.GetBalance(wallet.Address)).Balance;
        return wallet;
    }
}