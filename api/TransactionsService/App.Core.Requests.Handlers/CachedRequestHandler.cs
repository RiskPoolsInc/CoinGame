using App.Caching.Utilities;
using App.Core.Requests.Interfaces;
using App.Interfaces.Core.Requests;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace App.Core.Requests.Handlers {

public abstract class CachedHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICachedRequest
{
    private readonly IDistributedCache _memoryCache;

    protected CachedHandler(IDistributedCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        return _memoryCache.GetOrCreateAsync(request.CacheKey, () => HandleAsync(request, cancellationToken), cancellationToken);
    }

    protected abstract Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
}