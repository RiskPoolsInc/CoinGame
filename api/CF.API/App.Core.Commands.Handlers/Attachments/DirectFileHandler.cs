using App.Core.Commands.Attachments;
using App.Interfaces.Data.Storage;
using App.Interfaces.Security;

namespace App.Core.Commands.Handlers.Attachments; 

public class DirectFileHandler : IRequestHandler<DirectFileCommand, string> {
    private readonly INamedStorageResolver _storageResolver;

    public DirectFileHandler(INamedStorageResolver storageResolver,
                             IContextProvider      contextProvider,
                             IMapper               mapper) {
        _storageResolver = storageResolver;
    }

    public async Task<string> Handle(DirectFileCommand request, CancellationToken cancellationToken) {
        var pathComponents = request.FullPath.Split('/', '\\');
        var storage = _storageResolver.GetStorage(pathComponents[0]);
        var containerPath = Path.Combine(pathComponents.Skip(1).ToArray());

        var storageFile = storage.GetFile(containerPath);
        await storageFile.SaveStreamAsync(request.File.OpenReadStream(), cancellationToken, request.File.ContentType);

        return Path.Combine(pathComponents);
    }
}