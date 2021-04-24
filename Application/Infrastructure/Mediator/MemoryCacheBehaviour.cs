using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Internal;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Infrastructure.Mediator
{
    public class MemoryCacheBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheBehaviour(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is not ICacheable cacheable) 
                return await next();
            
            var cacheKey = typeof(TRequest)
                .FullName;
            
            if (_memoryCache.TryGetValue(cacheKey, out TResponse cachedResponse))
            {
                return cachedResponse;
            }
            
            var res = await next();
            
            _memoryCache
                .Set(cacheKey, res, TimeSpan.FromSeconds(cacheable.LifetimeSeconds()));
            return res;
        }
    }
}