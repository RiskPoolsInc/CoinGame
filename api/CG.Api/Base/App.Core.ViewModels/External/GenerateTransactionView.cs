namespace App.Core.ViewModels.External;

public class GenerateTransactionView {
    public bool SkipTransaction { get; set; }
    public int SkipTransactionReasonId { get; set; }
    public string Hash { get; set; }
    public decimal Fee { get; set; }
    public decimal Sum { get; set; }
    public string WalletFrom { get; set; }
    public TransactionReceiverView[] Receviers { get; set; }
}