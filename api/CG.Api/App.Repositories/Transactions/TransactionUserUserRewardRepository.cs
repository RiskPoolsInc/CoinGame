using App.Interfaces.Repositories.Transactions;

namespace App.Repositories.Transactions;

public class TransactionUserUserRewardRepository : AuditableRepository<TransactionUserReward>, ITransactionUserRewardRepository {
    public TransactionUserUserRewardRepository(IAppDbContext context) : base(context) {
    }
}