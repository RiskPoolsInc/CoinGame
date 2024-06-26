using App.Data.Entities.Payments;

namespace App.Interfaces.Repositories.Payments.TaskRequests;

public interface ITaskRequestPaymentRepository : IAuditableRepository<TransactionUserRefund> {
}