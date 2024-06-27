using App.Data.Entities.Payments;
using App.Data.Sql;
using App.Data.Sql.Core.Interfaces;

namespace App.Repositories.Transactions;

public class TransactionCreateGameRepository:AuditableRepository<TransactionCreateGame> {
    public TransactionCreateGameRepository(AppDbContext context) : base(context) {
    }
}