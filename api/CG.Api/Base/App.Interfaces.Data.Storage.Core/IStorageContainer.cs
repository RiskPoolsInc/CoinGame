namespace App.Interfaces.Data.Storage.Core; 

public interface IStorageContainer {
    Task CreateIfNotExistsAsync(IStorageRequestOptions options, CancellationToken cancellationToken);
    IStorageDirectory GetDirectoryReference(string     name);
    IStorageBlock GetStorageBlockReference(string      name);
}