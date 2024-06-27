using App.Interfaces.Data.Storage;
using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Blobs; 

public class NamedStorageResolver : INamedStorageResolver {
    private readonly IStorageClient _client;

    public NamedStorageResolver(IStorageClient client) {
        _client = client;
    }

    public IStorage GetStorage(string containerName) {
        return new NamedStorage(_client, containerName);
    }
}