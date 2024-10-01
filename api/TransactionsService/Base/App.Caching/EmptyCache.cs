﻿using Microsoft.Extensions.Caching.Distributed;

namespace App.Caching; 

public class EmptyCache : IDistributedCache {
    public byte[] Get(string key) {
        return null;
    }

    public Task<byte[]> GetAsync(string key, CancellationToken token = default) {
        return Task.FromResult<byte[]>(null);
    }

    public void Refresh(string key) {
    }

    public Task RefreshAsync(string key, CancellationToken token = default) {
        return Task.CompletedTask;
    }

    public void Remove(string key) {
    }

    public Task RemoveAsync(string key, CancellationToken token = default) {
        return Task.CompletedTask;
    }

    public void Set(string key, byte[] value, DistributedCacheEntryOptions options) {
    }

    public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default) {
        return Task.CompletedTask;
    }
}