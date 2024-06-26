using App.Data.Entities.Notifications;

namespace App.Repositories.Notifications; 

public class TaskFollowRepository : Repository<TaskFollow>, ITaskFollowRepository {
    public TaskFollowRepository(IAppDbContext context) : base(context) {
    }
}