namespace MultiLayerCache.CacheProviders;

public class CacheManager(ICacheProvider[] cacheProviders, ILogger<CacheManager> logger)
{
    public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> getFromDbFunction, TimeSpan expiry)
    {
        logger.LogInformation("Try to get value from cache: {key}", key);

        foreach (var cacheProvider in cacheProviders)
        {
            var cachedValue = await cacheProvider.GetAsync<T>(key);

            if (cachedValue != null)
            {
                logger.LogInformation("=====> HIT cache for {key} in {cacheProvider}", key, cacheProvider.GetType().Name);

                return cachedValue;
            }
            else
                logger.LogInformation("Cache wasn't hit for {key} in {cacheProvider}", key, cacheProvider.GetType().Name);
        }

        logger.LogInformation("Not found in cache: {key}", key);

        var result = await getFromDbFunction();

        for (int i = 0; i < cacheProviders.Length; i++)
        {
            var expirySeconds = (i * 2 == 0 ? 1 : i * 2) * expiry.TotalSeconds;

            await cacheProviders[i].SaveAsync(key, result, TimeSpan.FromSeconds(expirySeconds));
        }

        return result;
    }

    public async Task DeleteAsync(string key)
    {
        foreach (var cacheProvider in cacheProviders)
        {
            await cacheProvider.DeleteAsync(key);
        }
    }
}
