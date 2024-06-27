using App.Core.Requests.Attachments;
using App.Core.ViewModels.Attachments;
using App.Data.Entities.Attachments;
using App.Interfaces.Repositories.Attachments;

using MediatR;

namespace App.Core.Requests.Handlers.Attachments; 

public class GetObjectAttachmentsHandler : GetAttachmentsHandler<Attachment>,
                                           IRequestHandler<GetObjectAttachmentsRequest, AttachmentView[]> {
    public GetObjectAttachmentsHandler(IAttachmentRepository repository) : base(repository) {
    }

    public Task<AttachmentView[]> Handle(GetObjectAttachmentsRequest request, CancellationToken cancellationToken) {
        return GetAttachments(request, cancellationToken);
    }
}