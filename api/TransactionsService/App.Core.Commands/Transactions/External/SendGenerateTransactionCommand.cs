using App.Core.Commands.TransactionReceivers;
using App.Core.ViewModels.External;

namespace App.Core.Commands.Transactions;

public class SendGenerateTransactionCommand : BaseSendTransactionCommand {
    public TransactionReceiverModel[] Receivers { get; set; }
}