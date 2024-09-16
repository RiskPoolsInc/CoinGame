using App.Core.ViewModels.Dictionaries;

namespace App.Core.ViewModels.Wallets;

public class WalletView : BaseView {
    public string Address { get; set; }
    public WalletTypeView Type { get; set; }
}