using App.Core.ViewModels.Dictionaries;

namespace App.Core.ViewModels.Transactions;

public class TransactionRewardView : BaseView {
    public string WalletFrom { get; set; }
    public string TransactionHash { get; set; }
    public decimal Sum { get; set; }
    public decimal Fee { get; set; }
    public TransactionTypeView Type { get; set; }
    public TransactionStateView State { get; set; }
    public bool ExistInBlockChain { get; set; }
}