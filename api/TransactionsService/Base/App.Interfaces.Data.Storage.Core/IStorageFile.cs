namespace App.Interfaces.Data.Storage.Core; 

public interface IStorageFile {
    string Uri { get; }
    Task<bool> ExistsAsync(CancellationToken           cancellationToken);
    Task SaveAsync(byte[]                              content, CancellationToken cancellationToken, string contentType = null);
    Task<byte[]> LoadAsync(CancellationToken           cancellationToken);
    Task<string> GetContentTypeAsync(CancellationToken cancellationToken);
    Task<Stream> GetStreamAsync(CancellationToken      cancellationToken);
    Task SaveStreamAsync(Stream                        stream,      CancellationToken cancellationToken, string contentType = null);
    Task<string> GetLinkAsync(string                   displayName, CancellationToken cancellationToken, int    expiresIn   = 5);
    Task<bool> DeleteIfExistsAsync(CancellationToken   cancellationToken);
    Task MoveFromAsync(IStorageFile                    src, CancellationToken cancellationToken);
}