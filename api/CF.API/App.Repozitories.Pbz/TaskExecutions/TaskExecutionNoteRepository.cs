using App.Data.Entities.Pbz.TaskExecutions;
using App.Interfaces.Repositories.Pbz.TaskExecutions;

namespace App.Repozitories.Pbz.TaskExecutions {

public class TaskExecutionNoteRepository : ArchivableRepository<TaskExecutionNote>, ITaskExecutionNoteRepository
{
    public TaskExecutionNoteRepository(IPbzDbContext context) : base(context)
    {
    }
}
}