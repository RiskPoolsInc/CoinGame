namespace App.Interfaces.Data.Storage.Core; 

/// <summary>
///     File in storage directory
/// </summary>
public interface IStorageBlock {
    string Name { get; set; }
    public IStorageBlockProperties Properties { get; set; }
    Uri Uri { get; set; }
    string FileLocation { get; }
    Task<bool> ExistsAsync(CancellationToken      cancellationToken);
    Task<bool> ExistsAsync(IStorageRequestOptions options, CancellationToken cancellationToken);
    Task StartCopyAsync(IStorageBlock             blob,    CancellationToken cancellationToken);
    Task FetchAttributesAsync(CancellationToken   cancellationToken);

    Task UploadFromByteArrayAsync(byte[]                 buffer,
                                  int                    index,
                                  int                    count,
                                  IStorageRequestOptions options,
                                  CancellationToken      cancellationToken);

    Task DownloadToStreamAsync(Stream                     stream,  IStorageRequestOptions options, CancellationToken cancellationToken);
    Task<bool> DeleteIfExistsAsync(IStorageRequestOptions options, CancellationToken      cancellationToken);
    Task UploadFromStreamAsync(Stream                     content, IStorageRequestOptions options, CancellationToken cancellationToken);
}