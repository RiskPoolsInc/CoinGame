using App.Core.Enums;

namespace App.Core.Pipeline.Validators.Helpers;

public static class GameHelper
{
    public static bool NotCompletedGameStateId(int stateId)
    {
        var completedStates = new[]
        {
            (int)GameStateTypes.Completed,
            (int)GameStateTypes.Payed,
        };
        return !completedStates.Contains(stateId);
    }
}