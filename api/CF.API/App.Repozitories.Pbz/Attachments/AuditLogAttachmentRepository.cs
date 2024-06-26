using App.Data.Entities.Pbz.Attachments;
using App.Interfaces.Repositories.Pbz.Attachments;

namespace App.Repozitories.Pbz.Attachments {

public class AuditLogAttachmentRepository : AuditableRepository<AuditLogAttachment>, IAuditLogAttachmentRepository
{
    public AuditLogAttachmentRepository(IPbzDbContext context) : base(context) { }
}
}