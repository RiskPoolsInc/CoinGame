namespace App.Repozitories.Pbz.Tasks {

public class TaskStateTaskStateGroupRepository : Repository<TaskStateTaskStateGroup>, ITaskStateTaskStateGroupRepository
{
    public TaskStateTaskStateGroupRepository(IPbzDbContext context) : base(context)
    {
    }
}
}