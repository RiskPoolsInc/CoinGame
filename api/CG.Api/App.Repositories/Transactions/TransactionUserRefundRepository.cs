namespace App.Repositories.Transactions;

public class TransactionUserRefundRepository:AuditableRepository<TransactionUserRefund> {
    public TransactionUserRefundRepository(IAppDbContext context) : base(context) {
    }
}