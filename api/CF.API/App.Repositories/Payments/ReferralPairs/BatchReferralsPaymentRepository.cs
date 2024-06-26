using App.Data.Entities.Payments.ReferralPayments;
using App.Interfaces.Repositories.Payments.ReferralPayments;

namespace App.Repositories.Payments.ReferralPairs;

public class BatchReferralsPaymentRepository : AuditableRepository<BatchReferralPairPayment>, IBatchReferralPaymentRepository {
    public BatchReferralsPaymentRepository(IAppDbContext context) : base(context) {
    }
}