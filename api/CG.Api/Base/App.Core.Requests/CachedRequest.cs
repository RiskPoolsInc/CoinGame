using App.Core.Requests.Interfaces;
using MediatR;

using ICachedRequest = App.Interfaces.Core.Requests.ICachedRequest;

namespace App.Core.Requests {
    public abstract class CachedRequest<TResult> : IRequest<TResult>, ICachedRequest {
        public CachedRequest(string cacheKey) {
            CacheKey = cacheKey;
        }
        public virtual string CacheKey { get; }
    }
}
