using App.Core.Enums;

namespace App.Data.Entities.Payments;

public class TransactionCreateGame : Transaction {
    public override int TypeId => (int)TransactionTypes.CreateGame;
}