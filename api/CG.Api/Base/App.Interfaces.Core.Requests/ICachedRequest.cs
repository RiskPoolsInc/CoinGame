namespace App.Interfaces.Core.Requests; 

public interface ICachedRequest {
    string CacheKey { get; }
}