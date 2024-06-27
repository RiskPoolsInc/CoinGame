using App.Core.ViewModels.External;

namespace App.Core.Commands.Transactions;

public class SendTransactionRewardCommand : BaseSendTransactionCommand {
    public string ToWallet { get; set; }
}