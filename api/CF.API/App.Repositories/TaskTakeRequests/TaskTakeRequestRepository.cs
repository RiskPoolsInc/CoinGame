using App.Data.Entities.TaskTakeRequests;
using App.Interfaces.Repositories;

namespace App.Repositories.TaskTakeRequests;

public class TaskTakeRequestRepository : ArchivableRepository<TaskTakeRequest>, ITaskTakeRequestRepository {
    public TaskTakeRequestRepository(IAppDbContext context) : base(context) {
    }
}