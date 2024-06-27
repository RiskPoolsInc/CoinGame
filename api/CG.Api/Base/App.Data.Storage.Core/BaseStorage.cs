using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Core; 

public abstract class BaseStorage : IStorage {
    private readonly Lazy<IStorageContainer> _container;
    private readonly StorageRequestOptions _options;

    protected BaseStorage(IStorageClient client, string containerName) {
        _container = new Lazy<IStorageContainer>(() => client.GetContainerReference(containerName.ToLower()));

        _options = new StorageRequestOptions {
            RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(3), 3)
        };
    }

    private IStorageContainer Container => _container.Value;

    public IStorage GetDirectory(string name) {
        return new StorageDirectory(Container.GetDirectoryReference(name), Container);
    }

    public IStorage GetDirectory(DateTime date) {
        return GetDirectory(string.Format("{0:yyyy}/{0:MM}/{0:dd}", date));
    }

    public IStorageFile GetFile(string name) {
        return new StorageFile(Container.GetStorageBlockReference(name), Container);
    }

    public async Task<string[]> EnumerateFilesAsync(CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }

    public async Task ClearAsync(CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}