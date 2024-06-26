using App.Data.Entities.Payments.ReferralPayments;
using App.Interfaces.Repositories.Payments.ReferralPayments;

namespace App.Repositories.Payments.ReferralPairs;

public class TransactionReferralPaymentRepository : AuditableRepository<TransactionReferralPairPayment>,
                                                    ITransactionReferralPaymentRepository {
    public TransactionReferralPaymentRepository(IAppDbContext context) : base(context) {
    }
}