using System.Linq.Expressions;
using App.Core.Enums;
using App.Data.Criterias.Core;
using App.Data.Entities.Wallets;

namespace App.Data.Criterias.Games;

public class WalletHaveActiveGames : ACriteria<Wallet>
{
    private readonly Guid _walletId;

    public WalletHaveActiveGames(Guid walletId)
    {
        _walletId = walletId;
    }

    public override Expression<Func<Wallet, bool>> Build()
    {
        var completedGameStates = new[]
        {
            (int)GameStateTypes.Completed,
            (int)GameStateTypes.Payed,
        };
        return s => s.Games.Any(a => !completedGameStates.Contains(a.StateId) && a.WalletId == _walletId);
    }
}