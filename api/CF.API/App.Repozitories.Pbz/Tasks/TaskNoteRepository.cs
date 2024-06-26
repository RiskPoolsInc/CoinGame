namespace App.Repozitories.Pbz.Tasks {

public class TaskNoteRepository : ArchivableRepository<TaskNote>, ITaskNoteRepository
{
    public TaskNoteRepository(IPbzDbContext context) : base(context) { }
}
}