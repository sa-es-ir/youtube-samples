
using Microsoft.Extensions.Caching.Memory;
namespace MultiLayerCache.CacheProviders;

public class InMemoryCacheProvider(IMemoryCache memCache, ILogger<InMemoryCacheProvider> logger) : ICacheProvider
{
    public Task<bool> DeleteAsync(string key)
    {
        try
        {
            memCache.Remove(key);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error on delete cache key: {key}", key);
            return Task.FromResult(false);
        }
    }

    public Task SaveAsync<T>(string key, T value, TimeSpan expiry)
    {
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry
        };

        memCache.Set(key, value, options);

        logger.LogInformation("Data saved to in-memory cache, key: {key} expiry:{expiry}", key, expiry.TotalSeconds);

        return Task.CompletedTask;
    }

    public Task<T?> GetAsync<T>(string key)
    {
        if (memCache.TryGetValue(key, out T? cacheResult))
            logger.LogInformation("=====> HIT in-memory cache: {key}", key);

        return Task.FromResult(cacheResult);
    }
}
