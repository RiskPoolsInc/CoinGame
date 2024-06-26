using App.Common.Helpers;
using App.Core.Commands.Wallets;
using App.Core.ViewModels.Wallets;
using App.Data.Entities.Wallets;
using App.Interfaces.Repositories;
using App.Interfaces.Repositories.Wallets;

namespace App.Core.Commands.Handlers.Wallets;

public class UpdateWalletBalanceHandler : IRequestHandler<UpdateWalletBalanceCommand, WalletView> {
    private readonly IWalletRepository _walletRepository;

    public UpdateWalletBalanceHandler(IWalletRepository walletRepository) {
        _walletRepository = walletRepository;
    }

    public async Task<WalletView> Handle(UpdateWalletBalanceCommand request, CancellationToken cancellationToken) {
        var entity = await _walletRepository.FindAsync(request.Id, cancellationToken);
        entity.Balance = request.Balance;
        await _walletRepository.SaveAsync(cancellationToken);
        return await _walletRepository.Get(request.Id).SingleAsync<Wallet, WalletView>(cancellationToken);
    }
}