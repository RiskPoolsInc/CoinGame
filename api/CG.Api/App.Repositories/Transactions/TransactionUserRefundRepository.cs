using App.Interfaces.Repositories.Transactions;

namespace App.Repositories.Transactions;

public class TransactionUserRefundRepository:AuditableRepository<TransactionUserRefund>, ITransactionRefundRepository {
    public TransactionUserRefundRepository(IAppDbContext context) : base(context) {
    }
}