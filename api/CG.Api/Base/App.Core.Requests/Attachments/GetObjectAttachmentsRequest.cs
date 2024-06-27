using App.Security.Annotation;

namespace App.Core.Requests.Attachments; 

[Access]
public class GetObjectAttachmentsRequest : GetAttachmentsRequest {
    public GetObjectAttachmentsRequest(Guid id) : base(id) {
    }
}