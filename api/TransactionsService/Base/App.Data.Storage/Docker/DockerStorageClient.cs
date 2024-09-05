using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Docker; 

public class DockerStorageClient : IStorageClient {
    private readonly string _hostPath;
    private readonly string _hostUrl;

    public DockerStorageClient(string hostPath, string hostUrl) {
        _hostPath = hostPath;
        _hostUrl = hostUrl;
    }

    public IStorageContainer GetContainerReference(string name) {
        return new DockerStorageContainer(_hostPath, _hostUrl, name);
    }
}