namespace App.Repositories.Transactions;

public class TransactionUserRewardRepository : AuditableRepository<TransactionUserReward> {
    public TransactionUserRewardRepository(IAppDbContext context) : base(context) {
    }
}