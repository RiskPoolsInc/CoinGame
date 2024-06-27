using App.Core.Enums;

namespace App.Data.Entities.Payments;

public class TransactionGameDeposit : Transaction {
    public override int TypeId => (int)TransactionTypes.GameDeposit;
}