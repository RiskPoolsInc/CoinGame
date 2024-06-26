namespace App.Repositories.Dictionaries {

public class TaskTypeRepository : DictionaryRepository<TaskType>, ITaskTypeRepository
{
    public TaskTypeRepository(IAppDbContext context) : base(context)
    {
    }
}
}