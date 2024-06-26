using App.Data.Entities;
using App.Interfaces.Repositories;

namespace App.Repositories {

public class AuditLogRepository : Repository<AuditLog>, IAuditLogRepository
{
    public AuditLogRepository(IAppDbContext context) : base(context) { }
}
}