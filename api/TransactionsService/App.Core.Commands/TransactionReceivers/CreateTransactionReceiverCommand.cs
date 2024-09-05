namespace App.Core.Commands.TransactionReceivers;

public class CreateTransactionReceiverCommand {
    public Guid? WalletId { get; set; }
    public string Address { get; set; }
    public decimal Sum { get; set; }
}