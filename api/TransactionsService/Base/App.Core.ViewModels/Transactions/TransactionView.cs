using App.Core.ViewModels.Dictionaries;
using App.Core.ViewModels.External;
using App.Core.ViewModels.Wallets;

namespace App.Core.ViewModels.Transactions;

public class TransactionView : BaseView {
    public WalletView WalletFrom { get; set; }
    public string TransactionHash { get; set; }
    public decimal Sum { get; set; }
    public decimal Fee { get; set; }
    public TransactionTypeView Type { get; set; }
    public TransactionStateView State { get; set; }
    public bool ExistInBlockChain { get; set; }
    public TransactionReceiverView[] Receivers { get; set; }
}