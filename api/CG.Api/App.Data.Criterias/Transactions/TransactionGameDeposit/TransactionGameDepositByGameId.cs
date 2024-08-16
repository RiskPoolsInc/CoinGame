using System.Linq.Expressions;

using App.Data.Criterias.Core;

namespace App.Data.Criterias.Transactions.TransactionGameDeposit;

public class TransactionGameDepositByGameId : ACriteria<Entities.Transactions.TransactionGameDeposit> {
    private readonly Guid _gameId;

    public TransactionGameDepositByGameId(Guid gameId) {
        _gameId = gameId;
    }

    public override Expression<Func<Entities.Transactions.TransactionGameDeposit, bool>> Build() {
        return a => a.GameId == _gameId;
    }
}