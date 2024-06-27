using App.Core.ViewModels.Dictionaries;
using App.Core.ViewModels.Transactions;
using App.Core.ViewModels.Wallets;

namespace App.Core.ViewModels.Games;

public class GameView : BaseView {
    public WalletView Wallet { get; set; }
    public GameStateView State { get; set; }
    public GameResultView Result { get; set; }
    public int RoundQuantity { get; set; }
    public decimal RoundSum { get; set; }
    public GameRoundView[] Rounds { get; set; }
}