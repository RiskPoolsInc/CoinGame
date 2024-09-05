using App.Core.Enums;

namespace App.Data.Entities.Transactions;

public class TransactionRefund : Transaction {
    public override int TypeId => (int)TransactionTypes.Refund;
}