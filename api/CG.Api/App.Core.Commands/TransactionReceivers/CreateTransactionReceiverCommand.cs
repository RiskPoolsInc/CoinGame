namespace App.Core.Commands.TransactionReceivers;

public class CreateTransactionReceiverCommand {
    public string WalletHash { get; set; }
    public decimal Sum { get; set; }
}