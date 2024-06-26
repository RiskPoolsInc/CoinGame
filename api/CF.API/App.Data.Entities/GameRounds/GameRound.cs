using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.Games;

namespace App.Data.Entities.GameRounds;

public class GameRound : AuditableEntity {
    public Guid GameId { get; set; }
    public virtual Game Game { get; set; }
    public int Number { get; set; }
    public string GeneratedValue { get; set; }
    public int GameRoundResultId { get; set; }
    public virtual GameRoundResult GameRoundResult { get; set; }
}