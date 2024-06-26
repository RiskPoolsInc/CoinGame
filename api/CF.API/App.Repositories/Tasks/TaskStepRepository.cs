using App.Data.Entities.Tasks;
using App.Data.Sql.Core.Interfaces;
using App.Interfaces.Repositories.Tasks;

namespace App.Repositories.Tasks; 

public class TaskStepRepository: ArchivableRepository<TaskStep>, ITaskStepRepository {
    public TaskStepRepository(IAppDbContext context) : base(context) {
    }
}