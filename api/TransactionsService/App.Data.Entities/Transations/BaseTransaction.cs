using App.Core.Enums;

namespace App.Data.Entities.Transactions;

public class BaseTransaction : Transaction {
    public override int TypeId => (int)TransactionTypes.Base;
}