using App.Core.Requests.Attachments;
using App.Core.ViewModels;
using App.Interfaces.Data.Storage;

using MediatR;

namespace App.Core.Requests.Handlers.Attachments; 

public class GetDirectLinkHandler : IRequestHandler<GetDirectLinkRequest, ExternalLink> {
    private readonly INamedStorageResolver _storageResolver;

    public GetDirectLinkHandler(INamedStorageResolver storageResolver) {
        _storageResolver = storageResolver;
    }

    public async Task<ExternalLink> Handle(GetDirectLinkRequest request, CancellationToken cancellationToken) {
        var pathComponents = request.DirectLink.Split('/', '\\');
        var storage = _storageResolver.GetStorage(pathComponents[0]);
        var containerPath = string.Join("/", pathComponents.Skip(1).ToArray());

        var storageFile = storage.GetFile(containerPath);

        var sasUrl = await storageFile.GetLinkAsync(pathComponents.Last(), cancellationToken, 600);

        return new ExternalLink {
            Url = sasUrl
        };
    }
}