using App.Core.ViewModels.Transactions;

namespace App.Core.ViewModels.Wallets;

public class WalletBalanceView : BaseView {
    public string Address { get; set; }
    public bool IsBlocked { get; set; }
    public TransactionUserRefundView Refund { get; set; }
    public decimal Balance { get; set; }
}