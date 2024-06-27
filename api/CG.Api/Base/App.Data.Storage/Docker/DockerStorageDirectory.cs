using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Docker; 

public class DockerStorageDirectory : IStorageDirectory {
    private readonly string _basePath;
    private readonly string _hostPath;
    private readonly string _hostUrl;

    public DockerStorageDirectory(string hostPath, string hostUrl, string containerPath, string directoryName) {
        _hostPath = hostPath;
        _hostUrl = hostUrl;
        _basePath = Path.Combine(containerPath, directoryName);
    }

    public IStorageDirectory GetDirectoryReference(string name) {
        return new DockerStorageDirectory(_hostPath, _hostUrl, _basePath, name);
    }

    public IStorageBlock GetBlockBlobReference(string name) {
        return new DockerStorageFile(_hostPath, _hostUrl, _basePath, name);
    }
}