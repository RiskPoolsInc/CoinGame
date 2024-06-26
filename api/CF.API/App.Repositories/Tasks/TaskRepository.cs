using App.Data.Entities.Tasks;
using App.Interfaces.Repositories.Tasks;

namespace App.Repositories.Tasks;

public class TaskRepository : ArchivableRepository<TaskEntity>, ITaskRepository {
    public TaskRepository(IAppDbContext context) : base(context) {
    }
}