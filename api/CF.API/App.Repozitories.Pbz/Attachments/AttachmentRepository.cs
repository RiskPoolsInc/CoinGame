using App.Data.Entities.Pbz.Attachments;
using App.Interfaces.Repositories.Pbz.Attachments;

namespace App.Repozitories.Pbz.Attachments {

public class AttachmentRepository : AuditableRepository<Attachment>, IAttachmentRepository
{
    public AttachmentRepository(IPbzDbContext context) : base(context) { }
}
}