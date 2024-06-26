namespace App.Repozitories.Pbz.Dictionaries {

public class TaskTypeRepository : DictionaryRepository<TaskType>, ITaskTypeRepository
{
    public TaskTypeRepository(IPbzDbContext context) : base(context)
    {
    }
}
}