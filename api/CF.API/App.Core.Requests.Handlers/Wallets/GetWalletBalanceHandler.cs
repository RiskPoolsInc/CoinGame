using App.Common.Helpers;
using App.Core.Requests.Wallets;
using App.Core.ViewModels.Wallets;
using App.Data.Entities.Wallets;
using App.Interfaces.ExternalServices;
using App.Interfaces.Repositories;
using App.Interfaces.Repositories.Wallets;

using MediatR;

namespace App.Core.Requests.Handlers.Wallets;

public class GetWalletBalanceHandler : IRequestHandler<GetWalletBalanceRequest, WalletView> {
    private readonly IWalletRepository _walletRepository;
    private readonly IWalletService _walletService;

    public GetWalletBalanceHandler(IWalletRepository walletRepository, IWalletService walletService) {
        _walletRepository = walletRepository;
        _walletService = walletService;
    }

    public async Task<WalletView> Handle(GetWalletBalanceRequest request, CancellationToken cancellationToken) {
        // var wallet = await _walletRepository.FindAsync(request.WalletId!.Value, cancellationToken);
        // //TODO USE REALLY REQUEST
        // var balance = await _walletService.GetBalance(wallet.Address, cancellationToken);
        // wallet.Balance = balance;
        //
        // _walletRepository.Update(wallet);
        // await _walletRepository.SaveAsync(cancellationToken);
        return await _walletRepository.Get(request.WalletId.Value).SingleAsync<Wallet, WalletView>(cancellationToken);
    }
}