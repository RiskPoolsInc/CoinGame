using App.Core.Commands.Attachments;
using App.Core.Exceptions;
using App.Core.Requests.Attachments;
using App.Core.Requests.Handlers.Attachments;
using App.Core.ViewModels.Attachments;
using App.Interfaces.Repositories.Attachments;

namespace App.Core.Commands.Handlers.Attachments; 

public class DeleteAttachmentHandler : IRequestHandler<DeleteAttachmentCommand, AttachmentView> {
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly GetAttachmentHandler _getAttachmentHandler;

    public DeleteAttachmentHandler(GetAttachmentHandler getAttachmentHandler, IAttachmentRepository attachmentRepository) {
        _attachmentRepository = attachmentRepository;
        _getAttachmentHandler = getAttachmentHandler;
    }

    public async Task<AttachmentView> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken) {
        var model = await _getAttachmentHandler.Handle(new GetAttachmentRequest(request.Id), cancellationToken);

        if (model == null)
            throw new EntityNotFoundException("Attachment", request.Id);
        var entity = await _attachmentRepository.FindAsync(request.Id, cancellationToken);
        _attachmentRepository.Delete(entity);
        await _attachmentRepository.SaveAsync(cancellationToken);
        return model;
    }
}