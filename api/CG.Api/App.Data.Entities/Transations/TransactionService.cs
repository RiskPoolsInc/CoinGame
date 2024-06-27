using App.Core.Enums;

namespace App.Data.Entities.Transactions;

public class TransactionService : Transaction {
    public override int TypeId => (int)TransactionTypes.Service;
}