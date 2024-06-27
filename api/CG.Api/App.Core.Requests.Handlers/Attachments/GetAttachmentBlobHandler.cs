using App.Common.Helpers;
using App.Core.Exceptions;
using App.Core.Requests.Attachments;
using App.Core.ViewModels;
using App.Core.ViewModels.Attachments;
using App.Data.Entities.Attachments;
using App.Interfaces.Data.Storage;
using App.Interfaces.Repositories.Attachments;

using MediatR;

namespace App.Core.Requests.Handlers.Attachments; 

public class GetAttachmentBlobHandler : IRequestHandler<GetAttachmentBlobRequest, AttachmentFile> {
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IAttachmentStorage _attachmentStorage;

    public GetAttachmentBlobHandler(IAttachmentRepository attachmentRepository, IAttachmentStorage attachmentStorage) {
        _attachmentRepository = attachmentRepository;
        _attachmentStorage = attachmentStorage;
    }

    public async Task<AttachmentFile> Handle(GetAttachmentBlobRequest request, CancellationToken cancellationToken) {
        var attachment = await _attachmentRepository.Get(request.Id).SingleAsync<Attachment, ExternalFile>(cancellationToken);

        if (attachment == null)
            throw new EntityNotFoundException("Attachment", request.Id);

        // storage -> object type folder -> year -> month -> day -> blob           
        var fileRef = _attachmentStorage
                     .GetDirectory(attachment.ObjectType.ToString().ToLowerInvariant())
                     .GetDirectory(attachment.CreatedOn)
                     .GetFile(attachment.FileName);

        return new AttachmentFile {
            CreatedOn = attachment.CreatedOn,
            DisplayName = attachment.DisplayName,
            FileName = attachment.FileName,
            Id = attachment.Id,
            ObjectType = attachment.ObjectType,
            File = await fileRef.LoadAsync(cancellationToken),
            ContentType = await fileRef.GetContentTypeAsync(cancellationToken)
        };
    }
}