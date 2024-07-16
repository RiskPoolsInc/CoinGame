using App.Interfaces.Repositories.Transactions;

namespace App.Repositories.Transactions;

public class TransactionServiceRepository:AuditableRepository<TransactionService>, ITransactionServiceRepository {
    public TransactionServiceRepository(IAppDbContext context) : base(context) {
    }
}