using App.Core.Commands.TransactionReceivers;
using App.Core.ViewModels.External;
using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Transactions;

public abstract class CreateTransactionCommand : IRequest<TransactionView> {
    public Guid FromWalletId { get; set; }
    public TransactionReceiverModel[] Receviers { get; set; }
}