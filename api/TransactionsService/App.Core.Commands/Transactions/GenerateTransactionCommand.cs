using App.Core.ViewModels.External;

namespace App.Core.Commands.Transactions;

public class GenerateTransactionCommand : IRequest<GenerateTransactionView> {
    public string WalletFromAddress { get; set; }
    public TransactionReceiverView[] WalletsTo { get; set; }
}