using App.Core.Enums;

namespace App.Data.Entities.Transactions;

public class TransactionRefund : ATransaction {
    public override int TypeId => (int)TransactionTypes.Refund;
}