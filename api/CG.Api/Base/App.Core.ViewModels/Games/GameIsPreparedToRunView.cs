namespace App.Core.ViewModels.Games;

public class GameIsPreparedToRunView {
    public Guid Id { get; set; }
    public Guid WalletId { get; set; }
    public bool IsPreparedToRun { get; set; }
}