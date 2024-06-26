using App.Core.ViewModels.Attachments;

namespace App.Core.Commands.Attachments;

public class ImportTaskAttachCommand {
    public Guid Id { get; set; }
    public Guid AttachmentId { get; set; }
    public AttachmentFile FileAttachment { get; set; }
}