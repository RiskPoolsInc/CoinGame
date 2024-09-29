using App.Data.Entities.TransactionLogs;
using App.Interfaces.Repositories.LogEntities;

namespace App.Repositories.LogEntities;

public class LogEntityRepository : Repository<LogEntity>, ILogEntityRepository {
    public LogEntityRepository(IAppDbContext context) : base(context) {
    }
}