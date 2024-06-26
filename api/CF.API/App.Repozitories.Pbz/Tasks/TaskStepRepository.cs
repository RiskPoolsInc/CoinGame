namespace App.Repozitories.Pbz.Tasks; 

public class TaskStepRepository: Repository<TaskStep>, ITaskStepRepository {
    public TaskStepRepository(IPbzDbContext context) : base(context) {
    }
}