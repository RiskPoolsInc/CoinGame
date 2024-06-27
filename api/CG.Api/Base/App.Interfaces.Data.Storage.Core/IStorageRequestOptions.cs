namespace App.Interfaces.Data.Storage.Core; 

public interface IStorageRequestOptions {
    IExponentialRetry RetryPolicy { get; }
}