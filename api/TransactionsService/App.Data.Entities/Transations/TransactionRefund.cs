using App.Core.Enums;

namespace App.Data.Entities.Transactions;

public class TransactionRefund : BaseTransaction {
    public override int TypeId => (int)TransactionTypes.Refund;
}