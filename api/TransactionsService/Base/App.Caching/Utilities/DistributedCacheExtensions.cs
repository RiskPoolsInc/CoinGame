using Microsoft.Extensions.Caching.Distributed;

using Newtonsoft.Json;

namespace App.Caching.Utilities; 

public static class DistributedCacheExtensions {
    private static readonly DistributedCacheEntryOptions _options;
    private static readonly JsonSerializerSettings _settings;

    static DistributedCacheExtensions() {
        _settings = new JsonSerializerSettings {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore
        };

        _options = new DistributedCacheEntryOptions {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        };
    }

    public static TResult GetOrCreate<TResult>(this IDistributedCache cache, string key, DistributedCacheEntryOptions options,
                                               Func<TResult>          func) {
        var obj = default(TResult);
        string value;

        try {
            value = cache.GetString(key);

            if (value != null) {
                obj = JsonConvert.DeserializeObject<TResult>(value, _settings);
                return obj;
            }
        }
        catch {
        }

        obj = func();

        try {
            value = JsonConvert.SerializeObject(obj, _settings);
            cache.SetString(key, value, options);
        }
        catch {
        }

        return obj;
    }

    public static TResult GetOrCreate<TResult>(this IDistributedCache cache, string key, Func<TResult> func) {
        return cache.GetOrCreate(key, _options, func);
    }

    public static async Task<TResult> GetOrCreateAsync<TResult>(this IDistributedCache       cache,   string              key,
                                                                DistributedCacheEntryOptions options, Func<Task<TResult>> func,
                                                                CancellationToken            cancellationToken) {
        var obj = default(TResult);
        string value;

        try {
            value = await cache.GetStringAsync(key, cancellationToken);

            if (value != null) {
                obj = JsonConvert.DeserializeObject<TResult>(value, _settings);
                return obj;
            }
        }
        catch {
        }

        obj = await func();

        try {
            return await cache.SetAsync(key, obj, options, cancellationToken);
        }
        catch {
        }

        return obj;
    }

    public static async Task<TResult> SetAsync<TResult>(this IDistributedCache       cache,   string            key, TResult obj,
                                                        DistributedCacheEntryOptions options, CancellationToken cancellationToken) {
        var value = JsonConvert.SerializeObject(obj, _settings);
        await cache.SetStringAsync(key, value, options, cancellationToken);
        return obj;
    }

    public static Task<TResult> GetOrCreateAsync<TResult>(this IDistributedCache cache, string key, Func<Task<TResult>> func,
                                                          CancellationToken      cancellationToken) {
        return cache.GetOrCreateAsync(key, _options, func, cancellationToken);
    }
}