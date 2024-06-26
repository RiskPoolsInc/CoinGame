using App.Core.ViewModels.Wallets;

namespace App.Core.ViewModels.CustomerCompanies;

public class CustomerCompanyBaseView : BaseView {
    public string Name { get; set; }
    public string Site { get; set; }
    public Guid? WalletId { get; set; }
    public WalletView? Wallet { get; set; }
}