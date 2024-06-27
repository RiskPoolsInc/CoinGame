using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Wallets;

public class RefundCoinsCommand : IRequest<TransactionView> {
    public Guid WalletId { get; set; }
}