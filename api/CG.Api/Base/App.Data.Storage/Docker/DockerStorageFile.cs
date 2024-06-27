using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Docker; 

public class DockerStorageFile : IStorageBlock {
    private readonly FileInfo _currentFileInfo;
    private readonly string _hostPath;
    private readonly string _rootDirectory;

    public DockerStorageFile(string hostPath, string hostUrl, string rootDirectory, string name) {
        _hostPath = hostPath;
        _rootDirectory = rootDirectory;
        Name = name;
        _currentFileInfo = new FileInfo(Path.Combine(rootDirectory, Name));

        Uri = new Uri(_currentFileInfo.FullName.Replace(hostPath, hostUrl).Replace(@"\", @"/"));
        Properties = new DockerStorageBlockProperties();
    }

    private FileStream WriteStream => File.OpenWrite(_currentFileInfo.FullName);
    private FileStream ReadStream => File.OpenRead(_currentFileInfo.FullName);

    public string Name { get; set; }
    public IStorageBlockProperties Properties { get; set; }
    public Uri Uri { get; set; }
    public string FileLocation => Path.Combine(_rootDirectory, Name).Replace(_hostPath, "");

    public Task<bool> ExistsAsync(CancellationToken cancellationToken) {
        return Task.FromResult(_currentFileInfo.Exists);
    }

    public async Task<bool> ExistsAsync(IStorageRequestOptions options, CancellationToken cancellationToken) {
        var result = false;
        var retryCount = options?.RetryPolicy?.RetryCount ?? 0;
        var delay = options?.RetryPolicy?.FromSeconds ?? TimeSpan.Zero;

        do {
            try {
                result = await ExistsAsync(cancellationToken);
            }
            catch {
                await Task.Delay(delay, cancellationToken);
            }
        } while (!result && retryCount > 0);

        return result;
    }

    public Task StartCopyAsync(IStorageBlock blob, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }

    public Task FetchAttributesAsync(CancellationToken cancellationToken) {
        var mimeMapping = "application/unknown";

        try {
            mimeMapping = new CustomMimeMapping().GetContentType(_currentFileInfo);
        }
        catch (Exception e) {
            // ignored
        }

        Properties = new DockerStorageBlockProperties {
            ContentType = mimeMapping
        };
        return Task.CompletedTask;
    }

    public async Task UploadFromByteArrayAsync(byte[]            buffer, int index, int count, IStorageRequestOptions options,
                                               CancellationToken cancellationToken) {
        CreateDirIfNotExist(_currentFileInfo.Directory);

        await using var stream = WriteStream;
        await stream.WriteAsync(buffer, index, count, cancellationToken);
    }

    public async Task DownloadToStreamAsync(Stream stream, IStorageRequestOptions options, CancellationToken cancellationToken) {
        await using var currentStream = ReadStream;
        await currentStream.CopyToAsync(stream, cancellationToken);
    }

    public Task<bool> DeleteIfExistsAsync(IStorageRequestOptions options, CancellationToken cancellationToken) {
        if (_currentFileInfo.Exists)
            _currentFileInfo.Delete();
        return Task.FromResult(true);
    }

    public async Task UploadFromStreamAsync(Stream content, IStorageRequestOptions options, CancellationToken cancellationToken) {
        CreateDirIfNotExist(_currentFileInfo.Directory);

        await using var stream = WriteStream;

        await using (content) {
            await content.CopyToAsync(stream, cancellationToken);
        }
    }

    private void CreateDirIfNotExist(DirectoryInfo dir) {
        if (dir.Exists)
            return;
        var list = new List<DirectoryInfo> { dir };
        var temp = dir;

        while (true) {
            var parent = temp.Parent;

            if (parent.Exists)
                break;
            list.Add(parent);
            temp = parent;
        }

        list.Reverse();

        foreach (var directoryInfo in list)
            if (!directoryInfo.Exists)
                directoryInfo.Create();
    }
}