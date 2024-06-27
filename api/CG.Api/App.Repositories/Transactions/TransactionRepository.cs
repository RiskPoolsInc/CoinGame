using App.Data.Entities.Payments;
using App.Data.Sql;
using App.Data.Sql.Core.Interfaces;

namespace App.Repositories.Transactions;

public class TransactionRepository: AuditableRepository<Transaction> {
    public TransactionRepository(AppDbContext context) : base(context) {
    }
}