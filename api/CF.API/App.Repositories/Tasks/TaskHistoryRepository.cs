using App.Data.Entities.Tasks;
using App.Interfaces.Repositories.Tasks;

namespace App.Repositories.Tasks;

public class TaskHistoryRepository: Repository<TaskHistory>, ITaskHistoryRepository
{
    public TaskHistoryRepository(IAppDbContext context) : base(context)
    {
    }
}