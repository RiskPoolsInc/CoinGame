namespace App.Core.Commands.Payments.Abstractions;

public abstract class ACreateTransactionCommand
{
    public string TransactionHash { get; set; }
    public string AddressFrom { get; set; }
    public decimal Sum { get; set; }
    public decimal Fee { get; set; }
    public int? CoinType { get; set; } = 1;
}