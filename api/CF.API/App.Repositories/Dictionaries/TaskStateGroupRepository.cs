namespace App.Repositories.Dictionaries {

public class TaskStateGroupRepository : DictionaryRepository<TaskStateGroupType>, ITaskStateGroupRepository
{
    public TaskStateGroupRepository(IAppDbContext context) : base(context)
    {
    }
}
}