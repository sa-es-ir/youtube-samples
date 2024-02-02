using StackExchange.Redis;
using System.Text.Json;

namespace MultiLayerCache.CacheProviders;

public class RedisCacheProvider(IDatabase redis, ILogger<RedisCacheProvider> logger) : ICacheProvider
{
    public Task SaveAsync<T>(string key, T value, TimeSpan expiry)
    {
        return redis.StringSetAsync(key, JsonSerializer.Serialize(value), expiry);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        logger.LogInformation("Try to get value from redis cache: {key}", key);

        var data = await redis.StringGetAsync(key);

        if (data.IsNullOrEmpty)
        {
            logger.LogInformation("Cache not found in redis: {key}", key);
            return default;
        }

        logger.LogInformation("=====> HIT redis cache: {key}", key);

        return JsonSerializer.Deserialize<T>(data!);
    }

    public Task<bool> DeleteAsync(string key)
    {
        return redis.KeyDeleteAsync(key);
    }
}
