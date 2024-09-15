using App.Core.Commands.TransactionReceivers;
using App.Core.ViewModels.External;
using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Transactions;

public class GenerateTransactionCommand : IRequest<TransactionView> {
    public Guid WalletId { get; set; }
    public TransactionReceiverModel[] Receivers { get; set; }
}