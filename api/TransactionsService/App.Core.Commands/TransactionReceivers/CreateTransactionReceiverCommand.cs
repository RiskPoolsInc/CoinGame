namespace App.Core.Commands.TransactionReceivers;

public class CreateTransactionReceiverCommand {
    public Guid? WalletFromId { get; set; }
    public string ToAddress { get; set; }
    public decimal Sum { get; set; }
}