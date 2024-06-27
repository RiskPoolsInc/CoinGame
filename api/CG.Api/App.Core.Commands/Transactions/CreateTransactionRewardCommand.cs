using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Transactions;

public class CreateTransactionRewardCommand : IRequest<TransactionRewardView> {
    public Guid GameId { get; set; }

    public CreateTransactionRewardCommand() {
        
    }

    public CreateTransactionRewardCommand(Guid gameId) {
        GameId = gameId;
    }
}