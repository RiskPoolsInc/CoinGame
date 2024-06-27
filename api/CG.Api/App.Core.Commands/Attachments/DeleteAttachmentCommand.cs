using App.Core.ViewModels.Attachments;

namespace App.Core.Commands.Attachments;

public class DeleteAttachmentCommand : IRequest<AttachmentView> {
    public DeleteAttachmentCommand(Guid id) {
        Id = id;
    }

    public Guid Id { get; }
}