using App.Data.Entities.Notifications;

namespace App.Interfaces.Repositories.Notifications; 

public interface INotificationRepository : IAuditableRepository<Notification> {
}