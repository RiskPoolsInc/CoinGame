using App.Interfaces.Repositories.Transactions;

namespace App.Repositories.Transactions;

public class TransactionRepository : AuditableRepository<Transaction>, ITransactionRepository {
    public TransactionRepository(IAppDbContext context) : base(context) {
    }
}