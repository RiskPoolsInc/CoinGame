namespace App.Repozitories.Pbz.Dictionaries {

public class TaskStateTypeRepository : DictionaryRepository<TaskStateType>, ITaskStateTypeRepository
{
    public TaskStateTypeRepository(IPbzDbContext context) : base(context)
    {
    }
}
}