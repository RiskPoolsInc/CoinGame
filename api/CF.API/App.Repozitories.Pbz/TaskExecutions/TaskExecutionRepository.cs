using App.Data.Entities.Pbz.TaskExecutions;
using App.Interfaces.Repositories.Pbz.TaskExecutions;

namespace App.Repozitories.Pbz.TaskExecutions {

public class TaskExecutionRepository : ArchivableRepository<TaskExecution>, ITaskExecutionRepository
{
    public TaskExecutionRepository(IPbzDbContext context) : base(context)
    {
    }
}
}