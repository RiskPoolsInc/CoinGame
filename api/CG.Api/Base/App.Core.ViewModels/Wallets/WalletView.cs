using App.Core.ViewModels.Transactions;

namespace App.Core.ViewModels.Wallets;

public class WalletView : BaseView {
    public string Hash { get; set; }
    public bool IsBlocked { get; set; }
    public TransactionUserRefundView Refund { get; set; }
}