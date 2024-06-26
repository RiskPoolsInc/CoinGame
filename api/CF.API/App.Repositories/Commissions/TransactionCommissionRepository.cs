using App.Data.Entities.Commissions;
using App.Interfaces.Repositories;

namespace App.Repositories.Commissions;

public class TransactionCommissionRepository : Repository<TransactionCommission>, ITransactionCommissionRepository {
    public TransactionCommissionRepository(IAppDbContext context) : base(context) {
    }
}