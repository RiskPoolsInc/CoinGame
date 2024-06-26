using App.Core.ViewModels.Attachments;
using App.Security.Annotation;

namespace App.Core.Requests.Attachments; 

[Access]
public class GetAttachmentRequest : IRequest<AttachmentView> {
    public GetAttachmentRequest(Guid id) {
        Id = id;
    }

    public Guid Id { get; private set; }
}