using App.Core.ViewModels.External;

namespace App.Core.Commands.Transactions;

public class GenerateTransactionCommand : IRequest<GenerateTransactionView> {
    public Guid WalletFromId { get; set; }
    public TransactionReceiverView[] WalletsTo { get; set; }
}