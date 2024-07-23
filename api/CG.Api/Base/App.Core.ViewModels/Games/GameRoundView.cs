using App.Core.ViewModels.Dictionaries;

namespace App.Core.ViewModels.Games;

public class GameRoundView : BaseView {
    public Guid GameId { get; set; }
    public int Number { get; set; }
    public GameRoundResultView Result { get; set; }
}