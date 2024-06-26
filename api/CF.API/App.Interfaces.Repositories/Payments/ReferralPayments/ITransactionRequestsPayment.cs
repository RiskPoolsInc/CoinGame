﻿using App.Data.Entities.Payments.ReferralPayments;

namespace App.Interfaces.Repositories.Payments.ReferralPayments;

public interface ITransactionReferralPaymentRepository : IAuditableRepository<TransactionReferralPairPayment> {
}