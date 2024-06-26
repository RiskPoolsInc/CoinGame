using App.Data.Entities.Pbz.Notifications;
using App.Interfaces.Repositories.Pbz.Notifications;

namespace App.Repozitories.Pbz.Notifications {

public class NotificationRepository : AuditableRepository<Notification>, INotificationRepository
{
    public NotificationRepository(IPbzDbContext dbContext) : base(dbContext) { }
}
}