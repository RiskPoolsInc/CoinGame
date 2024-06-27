using App.Security.Annotation;

namespace App.Core.Requests.Attachments; 

[Access]
public class GetAttachmentLinkRequest : AttachmentLinkParams, IRequest<ExternalLink> {
    public GetAttachmentLinkRequest(Guid id, AttachmentLinkParams param) {
        Id = id;

        if (param == null)
            throw new ArgumentNullException(nameof(param));
        ExpiredIn = param.ExpiredIn;
    }

    public Guid Id { get; private set; }
}