using App.Core.Enums;

namespace App.Data.Entities.Transactions;

public class TransactionUserRefund : Transaction {
    public override int TypeId => (int)TransactionTypes.UserRefund;
}