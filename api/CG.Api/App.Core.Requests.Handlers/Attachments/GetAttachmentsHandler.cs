using App.Common.Helpers;
using App.Core.Requests.Attachments;
using App.Core.ViewModels.Attachments;
using App.Data.Criterias.Attachments;
using App.Data.Entities.Attachments;
using App.Interfaces.Repositories.Core;

namespace App.Core.Requests.Handlers.Attachments; 

public abstract class GetAttachmentsHandler<TEntity> where TEntity : Attachment {
    private readonly IRepository<TEntity> _attachmentRepository;

    public GetAttachmentsHandler(IRepository<TEntity> attachmentRepository) {
        _attachmentRepository = attachmentRepository;
    }

    protected async Task<AttachmentView[]> GetAttachments<TRequest>(TRequest request, CancellationToken cancellationToken)
        where TRequest : GetAttachmentsRequest {
        var result = await _attachmentRepository.Where(new AttachmentByObjectId<TEntity>(request.ObjectId).Build())
                                                .ToArrayAsync<Attachment, AttachmentView>(cancellationToken);
        return result;
    }
}