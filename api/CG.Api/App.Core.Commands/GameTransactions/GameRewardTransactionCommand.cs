using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Transactions;

public class GameRewardTransactionCommand : IRequest<TransactionView> {
    public Guid WalletId { get; set; }
}