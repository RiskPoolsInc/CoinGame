using App.Data.Entities.Payments.ReferralPayments;
using App.Interfaces.Repositories.Payments.ReferralPayments;

namespace App.Repositories.Payments.ReferralPairs;

public class ReferralPaymentRepository : AuditableRepository<ReferralPairPayment>, IReferralPaymentRepository {
    public ReferralPaymentRepository(IAppDbContext context) : base(context) {
    }
}