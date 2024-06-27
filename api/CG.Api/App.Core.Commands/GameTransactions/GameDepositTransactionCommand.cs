using App.Core.ViewModels.External;
using App.Core.ViewModels.Transactions;
using App.Data.Entities.Payments;

namespace App.Core.Commands.Transactions;

public class GameDepositTransactionCommand : IRequest<TransactionGameDepositView> {
    public Guid WalletId { get; set; }
    public Guid GameId { get; set; }
    public decimal Sum { get; set; }
}