namespace App.Repositories.Transactions;

public class TransactionRepository : AuditableRepository<Transaction> {
    public TransactionRepository(IAppDbContext context) : base(context) {
    }
}