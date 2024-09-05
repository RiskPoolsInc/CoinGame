using App.Core.ViewModels.External;
using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Transactions;

public abstract class CreateTransactionCommand: IRequest<TransactionView> {
    public Guid? FromWalletId { get; set; }
    public string FromWallet { get; set; }
    public TransactionReceiverView[] ToWallets { get; set; }
    public decimal Fee { get; set; }
}