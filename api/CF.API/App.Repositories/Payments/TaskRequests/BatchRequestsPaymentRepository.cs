using App.Data.Entities.Payments.TaskRequests;
using App.Interfaces.Repositories.Payments.TaskRequests;

namespace App.Repositories.Payments.TaskRequests;

public class BatchRequestsPaymentRepository : AuditableRepository<BatchTaskRequestsPayment>, IBatchRequestsPaymentRepository {
    public BatchRequestsPaymentRepository(IAppDbContext context) : base(context) {
    }
}