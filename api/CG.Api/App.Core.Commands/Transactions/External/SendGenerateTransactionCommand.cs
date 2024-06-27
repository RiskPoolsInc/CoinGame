using App.Core.ViewModels.External;

namespace App.Core.Commands.Transactions;

public class SendGenerateTransactionCommand : BaseSendTransactionCommand {
    public TransactionReceiverView[] WalletsTo { get; set; }
}