using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Coworking.Application.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public ResponseCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<string?> CacheResponseAsync(string key, CancellationToken cancellationToken = default)
        {
            return await _distributedCache.GetStringAsync(key, cancellationToken);
        }

        public async Task SetCacheValueAsync(string key, string value, int expiry,
            CancellationToken cancellationToken = default)
        {
            await _distributedCache.SetStringAsync(key, value, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expiry)
            }, cancellationToken);
        }

        public async Task ClearCacheAsync(string key, CancellationToken cancellationToken = default)
        {
            var result = await _distributedCache.GetStringAsync(key, cancellationToken);
            if (result is null) return;
            await _distributedCache.RemoveAsync(key, cancellationToken);
        }

        public string CreateCacheKey(string path, int locationId)
        {
            return path + "_x_" + locationId;
        }
    }
}