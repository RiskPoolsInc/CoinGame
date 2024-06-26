using App.Core.Enums;
using App.Security.Annotation;

namespace App.Core.Commands.Attachments {

[Access]
public class CreateAuditLogAttachCommand : AttachmentCommand
{
    public CreateAuditLogAttachCommand(Guid auditLogId, AttachModel model) : base(auditLogId, model)
    {
        Description = model.Description;
    }

    public CreateAuditLogAttachCommand(Guid auditLogId, AttachTempModel model) : base(auditLogId, model)
    {
        Description = model.Description;
    }

    public string Description { get; set; }

    public override ObjectTypes ObjectType => ObjectTypes.AuditLog;
}
}