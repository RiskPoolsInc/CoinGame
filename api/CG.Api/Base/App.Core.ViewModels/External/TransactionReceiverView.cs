namespace App.Core.ViewModels.External;

public class TransactionReceiverView : BaseView {
    public Guid TransactionId { get; set; }
    public string WalletHash { get; set; }
    public decimal Sum { get; set; }
    public int TypeId { get; set; }
    public virtual TransactionReceiverType Type { get; set; }
}