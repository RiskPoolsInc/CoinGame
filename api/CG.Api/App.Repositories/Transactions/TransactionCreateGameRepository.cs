using App.Data.Entities.Payments;
using App.Data.Sql;

namespace App.Repositories.Transactions;

public class TransactionCreateGameRepository:AuditableRepository<TransactionGameDeposit> {
    public TransactionCreateGameRepository(AppDbContext context) : base(context) {
    }
}