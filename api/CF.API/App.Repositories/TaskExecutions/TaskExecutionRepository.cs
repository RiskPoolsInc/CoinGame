using App.Data.Entities.TaskExecutions;
using App.Interfaces.Repositories.TaskExecutions;

namespace App.Repositories.TaskExecutions {

public class TaskExecutionRepository : ArchivableRepository<TaskExecution>, ITaskExecutionRepository
{
    public TaskExecutionRepository(IAppDbContext context) : base(context)
    {
    }
}
}