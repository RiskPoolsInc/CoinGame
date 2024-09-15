using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Wallets;

public class RefundCoinsCommand : IRequest<TransactionUserRefundView> {
    public RefundCoinsCommand(Guid walletId) {
        WalletId = walletId;
    }

    public Guid WalletId { get; set; }
}