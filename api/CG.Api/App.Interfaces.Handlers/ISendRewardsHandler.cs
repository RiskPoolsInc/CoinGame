using App.Core.Commands.Transactions;
using App.Core.ViewModels.Transactions;

using MediatR;

namespace App.Interfaces.Handlers;

public interface ISendRewardsHandler : IRequestHandler<SendRewardsCommand, TransactionRewardView[]> {
}