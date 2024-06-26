using App.Data.Entities.Attachments;
using App.Interfaces.Repositories.Attachments;

namespace App.Repositories.Attachments; 

public class TaskAttachmentRepository : AuditableRepository<TaskAttachment>, ITaskAttachmentRepository {
    public TaskAttachmentRepository(IAppDbContext context) : base(context) {
    }
}