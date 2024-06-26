using App.Data.Entities.Pbz.Attachments;
using App.Interfaces.Repositories.Pbz.Attachments;

namespace App.Repozitories.Pbz.Attachments {

public class TaskAttachmentRepository : AuditableRepository<TaskAttachment>, ITaskAttachmentRepository
{
    public TaskAttachmentRepository(IPbzDbContext context) : base(context) { }
}
}