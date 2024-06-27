using App.Data.Entities.Payments;

namespace App.Repositories.Transactions;

public class TransactionUserRefundRepository:AuditableRepository<TransactionUserRefund> {
    public TransactionUserRefundRepository(IAppDbContext context) : base(context) {
    }
}