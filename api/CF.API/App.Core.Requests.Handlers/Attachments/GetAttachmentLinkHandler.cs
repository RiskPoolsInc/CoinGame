using App.Common.Helpers;
using App.Core.Exceptions;
using App.Core.Requests.Attachments;
using App.Core.ViewModels;
using App.Data.Entities.Attachments;
using App.Interfaces.Data.Storage;
using App.Interfaces.Repositories.Attachments;

using MediatR;

namespace App.Core.Requests.Handlers.Attachments; 

public class GetAttachmentLinkHandler : IRequestHandler<GetAttachmentLinkRequest, ExternalLink> {
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IAttachmentStorage _attachmentStorage;

    public GetAttachmentLinkHandler(IAttachmentRepository attachmentRepository, IAttachmentStorage attachmentStorage) {
        _attachmentRepository = attachmentRepository;
        _attachmentStorage = attachmentStorage;
    }

    public async Task<ExternalLink> Handle(GetAttachmentLinkRequest request, CancellationToken cancellationToken) {
        var attachment = await _attachmentRepository.Get(request.Id).SingleAsync<Attachment, ExternalFile>(cancellationToken);

        if (attachment == null)
            throw new EntityNotFoundException("Attachment", request.Id);
        var entityDirectory = attachment.ObjectType.ToString().ToLowerInvariant();
        var entityDate = attachment.CreatedOn;
        var fileName = attachment.FileName;

        // storage -> object type folder -> year -> month -> day -> blob
        var sasUrl = await _attachmentStorage.GetDirectory(entityDirectory)
                                             .GetDirectory(entityDate)
                                             .GetFile(fileName)
                                             .GetLinkAsync(attachment.DisplayName, cancellationToken, request.ExpiredIn ?? 10);

        var result = new ExternalLink {
            Id = attachment.Id,
            CreatedOn = attachment.CreatedOn,
            Url = sasUrl //TODO Upgrade to sas link
        };
        return result;
    }
}