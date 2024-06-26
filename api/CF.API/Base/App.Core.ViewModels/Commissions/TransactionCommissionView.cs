namespace App.Core.ViewModels.Commissions;

public class TransactionCommissionView : BaseView {
    public Guid TransactionPaymentId { get; set; }
    public string Address { get; set; }
    public decimal CoinsCount { get; set; }
    public int? CoinsPercent { get; set; }
}