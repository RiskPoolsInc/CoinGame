using App.Data.Entities.Attachments;
using App.Interfaces.Repositories.Attachments;

namespace App.Repositories.Attachments {

public class TaskExecutionNoteAttachmentRepository : AuditableRepository<TaskExecutionNoteAttachment>, ITaskExecutionNoteAttachmentRepository
{
    public TaskExecutionNoteAttachmentRepository(IAppDbContext context) : base(context) { }
}
}