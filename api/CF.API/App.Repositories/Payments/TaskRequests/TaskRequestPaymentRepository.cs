using App.Data.Entities.Payments.TaskRequests;
using App.Interfaces.Repositories.Payments.TaskRequests;

namespace App.Repositories.Payments.TaskRequests;

public class TaskRequestPaymentRepository : AuditableRepository<TaskRequestPayment>, ITaskRequestPaymentRepository {
    public TaskRequestPaymentRepository(IAppDbContext context) : base(context) {
    }
}