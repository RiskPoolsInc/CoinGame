using App.Core.Enums;

namespace App.Data.Entities.Transactions;

public class Transaction : BaseTransaction {
    public override int TypeId => (int)TransactionTypes.Base;
}