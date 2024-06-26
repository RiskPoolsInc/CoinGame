using App.Data.Entities.Tasks;
using App.Interfaces.Repositories.Tasks;

namespace App.Repositories.Tasks;

public class TaskNoteRepository: ArchivableRepository<TaskNote>, ITaskNoteRepository
{
    public TaskNoteRepository(IAppDbContext context) : base(context)
    {
    }
}