using System.Linq.Expressions;

using App.Data.Criterias.Core;
using App.Data.Entities.Transactions;

namespace App.Data.Criterias.Transactions;

public class TransactionUserRewardByHash : ACriteria<TransactionUserReward> {
    private readonly string _hash;

    public TransactionUserRewardByHash(string hash) {
        _hash = hash;
    }

    public override Expression<Func<TransactionUserReward, bool>> Build() {
        return a => a.TransactionHash == _hash;
    }
}