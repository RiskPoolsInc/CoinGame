using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Core; 

public class StorageFile : IStorageFile {
    private readonly IStorageBlock _blob;
    private readonly IStorageContainer _container;
    private readonly IStorageRequestOptions _options;

    private bool _propertiesLoaded;

    public StorageFile(IStorageBlock blob, IStorageContainer container) {
        _blob = blob;
        _container = container;

        _options = new StorageRequestOptions {
            RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(3), 3)
        };
    }

    public string Uri => _blob.Name;

    public async Task MoveFromAsync(IStorageFile src, CancellationToken cancellationToken) {
        await RunAsync(() => MoveFromAsyncInternal(src as StorageFile, cancellationToken), cancellationToken);
    }

    public Task<bool> ExistsAsync(CancellationToken cancellationToken) {
        return RunAsync(() => _blob.ExistsAsync(_options, cancellationToken), cancellationToken);
    }

    public async Task<string> GetContentTypeAsync(CancellationToken cancellationToken) {
        if (!_propertiesLoaded) {
            await RunAsync(() => _blob.FetchAttributesAsync(cancellationToken), cancellationToken);
            _propertiesLoaded = true;
        }

        return _blob.Properties.ContentType;
    }

    public Task SaveAsync(byte[] content, CancellationToken cancellationToken, string contentType = null) {
        return RunAsync(() => {
            if (!string.IsNullOrWhiteSpace(contentType))
                _blob.Properties.ContentType = contentType;

            return _blob.UploadFromByteArrayAsync(content, 0, content.Length, _options,
                                                  cancellationToken);
        }, cancellationToken);
    }

    public async Task<byte[]> LoadAsync(CancellationToken cancellationToken = default) {
        using var stream = new MemoryStream();
        await RunAsync(() => _blob.DownloadToStreamAsync(stream, _options, cancellationToken), cancellationToken);
        return stream.ToArray();
    }

    public async Task<Stream> GetStreamAsync(CancellationToken cancellationToken = default) {
        var stream = new MemoryStream();
        await RunAsync(() => _blob.DownloadToStreamAsync(stream, _options, cancellationToken), cancellationToken);
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }

    public Task SaveStreamAsync(Stream content, CancellationToken cancellationToken, string contentType = null) {
        return RunAsync(() => {
            if (!string.IsNullOrWhiteSpace(contentType))
                _blob.Properties.ContentType = contentType;
            return _blob.UploadFromStreamAsync(content, _options, cancellationToken);
        }, cancellationToken);
    }

    public async Task<string> GetLinkAsync(string displayName, CancellationToken cancellationToken, int expiresIn = 5) {
        return _blob.Uri.ToString();
    }

    public Task<bool> DeleteIfExistsAsync(CancellationToken cancellationToken) {
        return _blob.DeleteIfExistsAsync(_options, cancellationToken);
    }

    public async Task StartCopyAsync(IStorageFile dest, CancellationToken cancellationToken) {
        await RunAsync(() => StartCopyAsyncInternal(dest as StorageFile, cancellationToken), cancellationToken);
    }

    #region Private Methods

    private async Task StartCopyAsyncInternal(StorageFile dest, CancellationToken cancellationToken) {
        if (dest == null)
            throw new ArgumentNullException(nameof(dest));

        if (await _blob.ExistsAsync(cancellationToken))
            await dest._blob.StartCopyAsync(_blob, cancellationToken);
    }

    private async Task MoveFromAsyncInternal(StorageFile src, CancellationToken cancellationToken) {
        if (src == null)
            throw new ArgumentNullException(nameof(src));

        await src.StartCopyAsyncInternal(this, cancellationToken);
        await src.DeleteIfExistsAsync(cancellationToken);
    }

    private async Task EsureThatContainerExists(CancellationToken cancellationToken) {
        await _container.CreateIfNotExistsAsync(_options, cancellationToken);
    }

    private async Task<T> RunAsync<T>(Func<Task<T>> func, CancellationToken cancellationToken) {
        await EsureThatContainerExists(cancellationToken);
        return await func();
    }

    private async Task RunAsync(Func<Task> func, CancellationToken cancellationToken) {
        await EsureThatContainerExists(cancellationToken);
        await func();
    }

    #endregion
}