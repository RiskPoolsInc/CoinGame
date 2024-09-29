using App.Core.Commands.Transactions;
using App.Core.ViewModels.Transactions;
using App.Interfaces.Handlers;

namespace App.Core.Commands.Handlers.Transactions;

public class SendRewardsHandler : ISendRewardsHandler {
    public SendRewardsHandler() {
    }

    public Task<TransactionRewardView[]> Handle(SendRewardsCommand request, CancellationToken cancellationToken) {
        return null;
    }
}