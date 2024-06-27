using App.Core.Enums;

namespace App.Data.Entities.Payments;

public class TransactionUserReward : Transaction {
    public override int TypeId => (int)TransactionTypes.UserReward;
}