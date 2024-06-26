namespace App.Repozitories.Pbz.Tasks {

public class TaskHistoryRepository : Repository<TaskHistory>, ITaskHistoryRepository
{
    public TaskHistoryRepository(IPbzDbContext context) : base(context) { }
}
}