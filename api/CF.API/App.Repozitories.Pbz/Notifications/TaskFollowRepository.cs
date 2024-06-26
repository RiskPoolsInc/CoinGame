using App.Interfaces.Repositories.Pbz.Notifications;

namespace App.Repozitories.Pbz.Notifications {

public class TaskFollowRepository : Repository<TaskFollow>, ITaskFollowRepository
{
    public TaskFollowRepository(IPbzDbContext context) : base(context)
    {
    }
}
}