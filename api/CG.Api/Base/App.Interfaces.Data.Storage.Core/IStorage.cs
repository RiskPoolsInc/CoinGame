namespace App.Interfaces.Data.Storage.Core; 

public interface IStorage {
    Task ClearAsync(CancellationToken                    cancellationToken);
    IStorage GetDirectory(string                         name);
    IStorage GetDirectory(DateTime                       date);
    IStorageFile GetFile(string                          name);
    Task<string[]> EnumerateFilesAsync(CancellationToken cancellationToken);
}