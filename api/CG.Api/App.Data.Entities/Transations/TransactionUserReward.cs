using App.Core.Enums;

namespace App.Data.Entities.Transactions;

public class TransactionUserReward : Transaction {
    public override int TypeId => (int)TransactionTypes.UserReward;
}