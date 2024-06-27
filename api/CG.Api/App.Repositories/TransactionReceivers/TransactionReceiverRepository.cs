using App.Data.Entities.Payments;
using App.Data.Entities.TransactionReceiver;
using App.Data.Sql;
using App.Data.Sql.Core.Interfaces;

namespace App.Repositories.Transactions;

public class TransactionReceiverRepository: AuditableRepository<TransactionReceiver> {
    public TransactionReceiverRepository(AppDbContext context) : base(context) {
    }
}