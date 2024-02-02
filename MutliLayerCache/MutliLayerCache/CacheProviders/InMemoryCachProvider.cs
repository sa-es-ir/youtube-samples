
using Microsoft.Extensions.Caching.Memory;
namespace MutliLayerCache.CacheProviders;

public class InMemoryCachProvider : ICacheProvider
{
    private readonly IMemoryCache _memCache;
    private readonly ILogger<InMemoryCachProvider> _logger;

    public InMemoryCachProvider(IMemoryCache memCache,
        ILogger<InMemoryCachProvider> logger)
    {
        _memCache = memCache;
        _logger = logger;
    }

    public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> getFromDbFunction,
        TimeSpan expiry, CancellationToken cancellationToken)
    {
        if (_memCache.TryGetValue(key, out T cacheResult))
            return cacheResult;

        var result = await getFromDbFunction();

        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry
        };

        _memCache.Set(key, result, options);

        return result;
    }

    public Task<bool> DeleteAsync(string key, CancellationToken cancellationToken)
    {
        try
        {
            _memCache.Remove(key);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on delete cache key: {key}", key);
            return Task.FromResult(false);
        }
    }
}
