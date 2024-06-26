using App.Data.Entities.Pbz.Attachments;
using App.Interfaces.Repositories.Pbz.Attachments;

namespace App.Repozitories.Pbz.Attachments {

public class TaskExecutionNoteAttachmentRepository : AuditableRepository<TaskExecutionNoteAttachment>, ITaskExecutionNoteAttachmentRepository
{
    public TaskExecutionNoteAttachmentRepository(IPbzDbContext context) : base(context) { }
}
}