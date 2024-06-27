using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Docker; 

public class DockerStorageContainer : IStorageContainer {
    private readonly string _containerPath;
    private readonly string _hostUrl;
    private readonly string _rootPath;

    public DockerStorageContainer(string rootPath, string hostUrl, string containerName) {
        _rootPath = rootPath;
        _hostUrl = hostUrl;
        _containerPath = Path.Combine(rootPath, containerName);
    }

    public Task CreateIfNotExistsAsync(IStorageRequestOptions options, CancellationToken cancellationToken) {
        if (!Directory.Exists(_containerPath))
            Directory.CreateDirectory(_containerPath);
        return Task.CompletedTask;
    }

    public IStorageDirectory GetDirectoryReference(string name) {
        return new DockerStorageDirectory(_rootPath, _hostUrl, _containerPath, name);
    }

    public IStorageBlock GetStorageBlockReference(string name) {
        return new DockerStorageFile(_rootPath, _hostUrl, _containerPath, name);
    }
}