using App.Core.Enums;

namespace App.Data.Entities.Transactions;

public class TransactionGameDeposit : Transaction {
    public override int TypeId => (int)TransactionTypes.GameDeposit;
}