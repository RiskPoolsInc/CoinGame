using App.Security.Annotation;

namespace App.Core.Requests.Attachments; 

[Access]
public class GetDirectLinkRequest : IRequest<ExternalLink> {
    public GetDirectLinkRequest(string directLink) {
        DirectLink = directLink;
    }

    public string DirectLink { get; set; }
}