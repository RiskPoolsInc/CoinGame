using App.Interfaces.Repositories.Transactions;

namespace App.Repositories.Transactions;

public class TransactionRewardRepository : AuditableRepository<BaseTransaction>, ITransactionRewardRepository {
    public TransactionRewardRepository(IAppDbContext context) : base(context) {
    }
}