namespace App.Core.Commands.Transactions;

public abstract class BaseSendTransactionCommand {
    public string FromWallet { get; set; }
    public string PrivateKey { get; set; }
}