using App.Data.Entities.TaskExecutions;
using App.Interfaces.Repositories.TaskExecutions;

namespace App.Repositories.TaskExecutions {

public class TaskExecutionNoteRepository : ArchivableRepository<TaskExecutionNote>, ITaskExecutionNoteRepository
{
    public TaskExecutionNoteRepository(IAppDbContext context) : base(context)
    {
    }
}
}