namespace App.Repozitories.Pbz.Dictionaries {

public class TaskStateGroupRepository : DictionaryRepository<TaskStateGroupType>, ITaskStateGroupRepository
{
    public TaskStateGroupRepository(IPbzDbContext context) : base(context)
    {
    }
}
}