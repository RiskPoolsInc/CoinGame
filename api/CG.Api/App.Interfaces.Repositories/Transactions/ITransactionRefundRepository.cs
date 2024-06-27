using App.Data.Entities.Payments;

namespace App.Interfaces.Repositories.Transactions;

public interface ITransactionRefundRepository : IAuditableRepository<TransactionUserRefund> {
}