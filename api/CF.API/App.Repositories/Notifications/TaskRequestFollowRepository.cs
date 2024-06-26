using App.Data.Entities.Notifications;

namespace App.Repositories.Notifications; 

public class TaskRequestFollowRepository : Repository<TaskRequestFollow>, ITaskRequestFollowRepository {
    public TaskRequestFollowRepository(IAppDbContext context) : base(context) {
    }
}