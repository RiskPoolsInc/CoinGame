using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Transactions;

public class GameServiceTransactionComand : IRequest<TransactionServiceView> {
    public Guid WalletId { get; set; }
    public Guid GameId { get; set; }
}