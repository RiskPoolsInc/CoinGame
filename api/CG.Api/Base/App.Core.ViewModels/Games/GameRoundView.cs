using App.Core.ViewModels.Dictionaries;

namespace App.Core.ViewModels.Games;

public class GameRoundView : BaseView {
    public Guid GameId { get; set; }
    public int Number { get; set; }
    public string HashForNumber { get; set; }
    public decimal CurrentGameRoundSum { get; set; }
    public int RoundNumber { get; set; }
    public GameRoundResultView Result { get; set; }
}