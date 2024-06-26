namespace App.Repositories.Dictionaries {

public class TaskActionTypeRepository : DictionaryRepository<TaskActionType>, ITaskActionTypeRepository
{
    public TaskActionTypeRepository(IAppDbContext context) : base(context)
    {
    }
}
}