namespace App.Repozitories.Pbz.Tasks {

public class TaskRepository : ArchivableRepository<TaskEntity>, ITaskRepository
{
    public TaskRepository(IPbzDbContext context) : base(context) { }
}
}