using System.Threading;
using System.Threading.Tasks;

namespace Coworking.Application.Services
{
    public interface IResponseCacheService
    {
        Task<string?> CacheResponseAsync(string key, CancellationToken cancellationToken = default);
        Task SetCacheValueAsync(string key, string value, int expiry, CancellationToken cancellationToken = default);
        Task ClearCacheAsync(string key, CancellationToken cancellationToken = default);
        string CreateCacheKey(string path, int locationId);
    }
}