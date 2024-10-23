using App.Core.ViewModels.Wallets;

namespace App.Core.Commands.Wallets;

public class UpdateWalletBalanceCommand : IRequest<WalletView> {
    public Guid WalletId { get; set; }

    public UpdateWalletBalanceCommand() {
    }

    public UpdateWalletBalanceCommand(Guid walletId) {
        WalletId = walletId;
    }
}