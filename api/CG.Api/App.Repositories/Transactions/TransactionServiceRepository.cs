using App.Data.Entities.Payments;
using App.Data.Sql;
using App.Data.Sql.Core.Interfaces;

namespace App.Repositories.Transactions;

public class TransactionServiceRepository:AuditableRepository<TransactionService> {
    public TransactionServiceRepository(AppDbContext context) : base(context) {
    }
}