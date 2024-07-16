using App.Interfaces.Repositories.Transactions;

namespace App.Repositories.Transactions;

public class TransactionUserRewardRepository : AuditableRepository<TransactionUserReward>, ITransactionRewardRepository {
    public TransactionUserRewardRepository(IAppDbContext context) : base(context) {
    }
}