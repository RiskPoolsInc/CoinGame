using App.Data.Entities.TaskExecutions;

namespace App.Interfaces.Repositories.TaskExecutions {

public interface ITaskExecutionRepository : IArchivableRepository<TaskExecution>
{
}
}