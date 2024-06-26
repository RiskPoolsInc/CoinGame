namespace App.Repositories.Dictionaries;

public class TaskExecutionStateRepository : DictionaryRepository<TaskExecutionState>, ITaskExecutionStateRepository
{
    public TaskExecutionStateRepository(IAppDbContext context) : base(context)
    {
    }
}