using StackExchange.Redis;
using System.Text.Json;

namespace MultiLayerCache.CacheProviders;

public class RedisCacheProvider(IDatabase Redis, ILogger<RedisCacheProvider> logger) : ICacheProvider
{
    public async Task SaveAsync<T>(string key, T value, TimeSpan expiry)
    {
        await Redis.StringSetAsync(key, JsonSerializer.Serialize(value), expiry);
        logger.LogInformation("Data saved to redis cache, key: {key} expiry:{expiry}", key, expiry.TotalSeconds);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var data = await Redis.StringGetAsync(key);

        if (data.IsNullOrEmpty)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(data!);
    }

    public Task<bool> DeleteAsync(string key)
    {
        return Redis.KeyDeleteAsync(key);
    }
}
