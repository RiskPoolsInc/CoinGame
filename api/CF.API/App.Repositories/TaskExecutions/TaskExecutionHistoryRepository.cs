using App.Data.Entities.TaskExecutions;
using App.Interfaces.Repositories.TaskExecutions;

namespace App.Repositories.TaskExecutions; 

public class TaskExecutionHistoryRepository: ArchivableRepository<TaskExecutionHistory>, ITaskExecutionHistoryRepository {
    public TaskExecutionHistoryRepository(IAppDbContext context) : base(context) {
    }
}