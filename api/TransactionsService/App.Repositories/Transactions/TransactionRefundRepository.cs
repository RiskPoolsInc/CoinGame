using App.Interfaces.Repositories.Transactions;

namespace App.Repositories.Transactions;

public class TransactionRefundRepository:AuditableRepository<TransactionRefund>, ITransactionRefundRepository {
    public TransactionRefundRepository(IAppDbContext context) : base(context) {
    }
}