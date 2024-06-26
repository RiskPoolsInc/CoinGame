using App.Data.Entities.Pbz.TaskExecutions;
using App.Data.Sql.Core.Interfaces;
using App.Interfaces.Repositories.Pbz.TaskExecutions;

namespace App.Repozitories.Pbz.TaskExecutions; 

public class TaskExecutionHistoryRepository: ArchivableRepository<TaskExecutionHistory>, ITaskExecutionHistoryRepository {
    public TaskExecutionHistoryRepository(IPbzDbContext context) : base(context) {
    }
}