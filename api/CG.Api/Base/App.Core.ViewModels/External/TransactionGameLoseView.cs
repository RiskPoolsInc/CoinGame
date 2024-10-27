namespace App.Core.ViewModels.External;

public class TransactionGameLoseView {
    public string Hash { get; set; }
    public decimal Sum { get; set; }
    public string WalletFrom { get; set; }
    public string ReceiverAddress { get; set; }
    public decimal Fee { get; set; }
    public Guid GameId { get; set; }
}