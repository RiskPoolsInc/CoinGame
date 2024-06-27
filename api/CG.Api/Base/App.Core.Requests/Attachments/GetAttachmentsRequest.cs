using App.Core.ViewModels.Attachments;

namespace App.Core.Requests.Attachments; 

public abstract class GetAttachmentsRequest : IRequest<AttachmentView[]> {
    public GetAttachmentsRequest(Guid id) {
        ObjectId = id;
    }

    public Guid ObjectId { get; private set; }
}