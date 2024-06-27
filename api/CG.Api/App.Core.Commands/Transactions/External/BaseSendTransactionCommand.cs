namespace App.Core.Commands.Transactions;

public abstract class BaseSendTransactionCommand {
    public string WalletFrom { get; set; }
    public string PrivateKey { get; set; }
}