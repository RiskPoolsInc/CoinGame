using App.Security.Annotation;

namespace App.Core.Requests.Attachments; 

[Access]
public class GetIncomingAttachmentsRequest : GetAttachmentsRequest {
    public GetIncomingAttachmentsRequest(Guid id) : base(id) {
    }
}