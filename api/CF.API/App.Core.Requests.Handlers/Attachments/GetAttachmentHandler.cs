using App.Common.Helpers;
using App.Core.Exceptions;
using App.Core.Requests.Attachments;
using App.Core.ViewModels.Attachments;
using App.Data.Entities.Attachments;
using App.Interfaces.Repositories.Attachments;

using MediatR;

namespace App.Core.Requests.Handlers.Attachments; 

public class GetAttachmentHandler : IRequestHandler<GetAttachmentRequest, AttachmentView> {
    private readonly IAttachmentRepository _attachmentRepository;

    public GetAttachmentHandler(IAttachmentRepository attachmentRepository) {
        _attachmentRepository = attachmentRepository;
    }

    public async Task<AttachmentView> Handle(GetAttachmentRequest request, CancellationToken cancellationToken) {
        var model = await _attachmentRepository.Get(request.Id).SingleAsync<Attachment, AttachmentView>(cancellationToken);

        if (model == null)
            throw new EntityNotFoundException("Attachment", request.Id);
        return model;
    }
}