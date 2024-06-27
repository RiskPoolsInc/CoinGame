using App.Data.Entities.Attachments;
using App.Interfaces.Repositories.Attachments;

namespace App.Repositories.Attachments; 

public class AttachmentRepository : AuditableRepository<Attachment>, IAttachmentRepository {
    public AttachmentRepository(IAppDbContext context) : base(context) {
    }
}