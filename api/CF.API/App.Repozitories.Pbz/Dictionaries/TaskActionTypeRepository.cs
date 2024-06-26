namespace App.Repozitories.Pbz.Dictionaries {

public class TaskActionTypeRepository : DictionaryRepository<TaskActionType>, ITaskActionTypeRepository
{
    public TaskActionTypeRepository(IPbzDbContext context) : base(context)
    {
    }
}
}