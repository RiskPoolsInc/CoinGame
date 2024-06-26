namespace App.Repositories.Dictionaries {

public class TaskStateTypeRepository : DictionaryRepository<TaskStateType>, ITaskStateTypeRepository
{
    public TaskStateTypeRepository(IAppDbContext context) : base(context)
    {
    }
}
}