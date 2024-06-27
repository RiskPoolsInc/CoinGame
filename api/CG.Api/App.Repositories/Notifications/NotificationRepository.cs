using App.Data.Entities.Notifications;

namespace App.Repositories.Notifications; 

public class NotificationRepository : AuditableRepository<Notification>, INotificationRepository {
    public NotificationRepository(IAppDbContext dbContext) : base(dbContext) {
    }
}