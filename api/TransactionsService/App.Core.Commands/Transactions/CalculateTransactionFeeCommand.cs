using App.Core.Commands.TransactionReceivers;
using App.Core.ViewModels.External;

namespace App.Core.Commands.Transactions;

public class CalculateTransactionFeeCommand : IRequest<TransactionFeeView> {
    public Guid WalletId { get; set; }
    public TransactionReceiverModel[] Receivers { get; set; }
}