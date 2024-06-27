using App.Core.ViewModels.External;
using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Transactions;

public class GameDepositTransactionCommand: IRequest<TransactionView> {
    public Guid WalletId { get; set; }
}