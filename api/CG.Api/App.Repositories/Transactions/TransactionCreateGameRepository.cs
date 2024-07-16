using App.Data.Sql;
using App.Interfaces.Repositories.Transactions;

namespace App.Repositories.Transactions;

public class TransactionCreateGameRepository:AuditableRepository<TransactionGameDeposit>, ITransactionGameDepositRepository {
    public TransactionCreateGameRepository(IAppDbContext context) : base(context) {
    }
}