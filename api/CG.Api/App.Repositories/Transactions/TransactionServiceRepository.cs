namespace App.Repositories.Transactions;

public class TransactionServiceRepository:AuditableRepository<TransactionService> {
    public TransactionServiceRepository(IAppDbContext context) : base(context) {
    }
}