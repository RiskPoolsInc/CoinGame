using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Transactions;

public class SendTransactionRewardCommand : IRequest<TransactionRewardView> {
    public Guid GameId { get; set; }

    public SendTransactionRewardCommand() {
        
    }

    public SendTransactionRewardCommand(Guid gameId) {
        GameId = gameId;
    }
}