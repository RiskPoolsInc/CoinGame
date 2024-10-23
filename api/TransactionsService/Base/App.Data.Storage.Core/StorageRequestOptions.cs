using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Core; 

public class StorageRequestOptions : IStorageRequestOptions {
    public IExponentialRetry RetryPolicy { get; set; }
}