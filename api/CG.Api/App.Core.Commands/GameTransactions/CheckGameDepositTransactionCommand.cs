using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Transactions;

public class CheckGameDepositTransactionCommand : IRequest<TransactionGameDepositView> {
    public Guid WalletId { get; set; }
    public Guid GameId { get; set; }

    public CheckGameDepositTransactionCommand() {
        
    }

    public CheckGameDepositTransactionCommand(Guid gameId) {
        GameId = gameId;
    }
}