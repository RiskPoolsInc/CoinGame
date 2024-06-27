namespace App.Interfaces.Data.Storage.Core; 

public interface IExponentialRetry {
    TimeSpan FromSeconds { get; }
    int RetryCount { get; }
}