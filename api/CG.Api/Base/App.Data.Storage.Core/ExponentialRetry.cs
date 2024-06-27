using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Core; 

public class ExponentialRetry : IExponentialRetry {
    public ExponentialRetry() {
    }

    public ExponentialRetry(TimeSpan fromSeconds, int retryCount) {
        FromSeconds = fromSeconds;
        RetryCount = retryCount;
    }

    public TimeSpan FromSeconds { get; }
    public int RetryCount { get; }
}