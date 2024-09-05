using App.Data.Entities.TransactionReceivers;
using App.Interfaces.Repositories.TransactionReceivers;

namespace App.Repositories.Transactions;

public class TransactionReceiverRepository : AuditableRepository<TransactionReceiver>, ITransactionReceiverRepository {
    public TransactionReceiverRepository(IAppDbContext context) : base(context) {
    }
}