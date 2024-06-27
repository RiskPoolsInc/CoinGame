using App.Data.Entities.Attachments;
using App.Interfaces.Repositories.Attachments;

namespace App.Repositories.Attachments {

public class AuditLogAttachmentRepository : AuditableRepository<AuditLogAttachment>, IAuditLogAttachmentRepository
{
    public AuditLogAttachmentRepository(IAppDbContext context) : base(context) { }
}
}