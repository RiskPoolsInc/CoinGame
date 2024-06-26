using App.Data.Entities.Payments.TaskRequests;
using App.Interfaces.Repositories.Payments.TaskRequests;

namespace App.Repositories.Payments.TaskRequests;

public class TransactionRequestsPaymentRepository : AuditableRepository<TransactionTaskRequestPayment>,
                                                    ITransactionRequestsPaymentRepository {
    public TransactionRequestsPaymentRepository(IAppDbContext context) : base(context) {
    }
}