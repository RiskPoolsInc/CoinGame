using App.Core.ViewModels.Dictionaries;

namespace App.Core.ViewModels.Wallets;

public class WalletBalanceView : WalletView {
    public decimal? Balance { get; set; }
}