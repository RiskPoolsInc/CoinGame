using App.Data.Entities.AutoPaymentServiceLogs;
using App.Interfaces.Repositories.AutoPaymentServiceLogs;

namespace App.Repositories.AutoPaymentServiceLogs;

public class AutoPaymentServiceLogRepository : Repository<AutoPaymentServiceLog>, IAutoPaymentServiceLogRepository {
    public AutoPaymentServiceLogRepository(IAppDbContext context) : base(context) {
    }
}