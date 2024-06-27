using App.Data.Entities.Payments;
using App.Data.Entities.TransactionReceiver;
using App.Data.Sql;
using App.Data.Sql.Core.Interfaces;
using App.Interfaces.Repositories.TransactionReceivers;

namespace App.Repositories.Transactions;

public class TransactionReceiverRepository : AuditableRepository<TransactionReceiver>, ITransactionReceiverRepository {
    public TransactionReceiverRepository(IAppDbContext context) : base(context) {
    }
}