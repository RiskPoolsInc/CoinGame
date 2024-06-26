using App.Data.Entities.Tasks;

namespace App.Interfaces.Repositories.Tasks {

public interface ITaskRepository : IArchivableRepository<TaskEntity>
{
}
}