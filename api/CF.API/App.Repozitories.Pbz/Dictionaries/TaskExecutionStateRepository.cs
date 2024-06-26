namespace App.Repozitories.Pbz.Dictionaries;

public class TaskExecutionStateRepository : DictionaryRepository<TaskExecutionState>, ITaskExecutionStateRepository
{
    public TaskExecutionStateRepository(IPbzDbContext context) : base(context)
    {
    }
}