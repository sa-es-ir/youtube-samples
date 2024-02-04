
using Microsoft.Extensions.Caching.Memory;
namespace MultiLayerCache.CacheProviders;

public class InMemoryCacheProvider(IMemoryCache memCache, ILogger<InMemoryCacheProvider> logger) : ICacheProvider
{
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

        memCache.TryGetValue(key, out T? cacheResult);

        return Task.FromResult(cacheResult);
    }

    public Task<bool> DeleteAsync(string key)
    {
        memCache.Remove(key);
        return Task.FromResult(true);
    }
}
