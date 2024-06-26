using App.Data.Entities.Pbz;
using App.Interfaces.Repositories.Pbz;

namespace App.Repozitories.Pbz {

public class AuditLogRepository : Repository<AuditLog>, IAuditLogRepository
{
    public AuditLogRepository(IPbzDbContext context) : base(context) { }
}
}