
using Microsoft.Extensions.Caching.Memory;
namespace MultiLayerCache.CacheProviders;

public class InMemoryCacheProvider : ICacheProvider
{
    private readonly IMemoryCache _memCache;
    private readonly ILogger<InMemoryCacheProvider> _logger;

    public InMemoryCacheProvider(IMemoryCache memCache,
        ILogger<InMemoryCacheProvider> logger)
    {
        _memCache = memCache;
        _logger = logger;
    }

    public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> getFromDbFunction,
        TimeSpan expiry, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to get value from in-memory cache: {key}", key);

        if (_memCache.TryGetValue(key, out T? cacheResult))
        {
            _logger.LogInformation("=====> HIT in-memory cache: {key}", key);
            return cacheResult!;
        }

        _logger.LogInformation("Cache not found for in-memory cache: {key}", key);

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

    public Task SaveAsync<T>(string key, T value, TimeSpan expiry, CancellationToken cancellationToken)
    {
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry
        };

        _memCache.Set(key, value, options);

        return Task.CompletedTask;
    }

    public Task<T?> GetAsync<T>(string key)
    {
        if (_memCache.TryGetValue(key, out T? cacheResult))
            _logger.LogInformation("=====> HIT in-memory cache: {key}", key);

        return Task.FromResult(cacheResult);
    }
}
