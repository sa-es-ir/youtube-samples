namespace MultiLayerCache.CacheProviders;

public class CacheManager(IEnumerable<ICacheProvider> cacheProviders, ILogger<CacheManager> logger)
{
    public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> getFromDbFunction, TimeSpan expiry)
    {
        foreach (var cacheProvider in cacheProviders)
        {
            logger.LogInformation("Try to get value from cache: {key} in {cacheProvider}", key, cacheProvider.GetType().Name);
            var cachedValue = await cacheProvider.GetAsync<T>(key);

            if (cachedValue != null)
            {
                logger.LogInformation("*****> HIT for {key} in {cacheProvider}", key, cacheProvider.GetType().Name);
                return cachedValue;
            }
            else
                logger.LogInformation("----->Cache wasn't hit for {key} in {cacheProvider}", key, cacheProvider.GetType().Name);
        }

        logger.LogInformation("====> Not found in any cache: {key}", key);

        var result = await getFromDbFunction();

        var providerList = cacheProviders.ToList();
        for (int i = 0; i < providerList.Count; i++)
        {
            var expirySeconds = (i * 2 == 0 ? 1 : i * 2) * expiry.TotalSeconds;

            await providerList[i].SaveAsync(key, result, TimeSpan.FromSeconds(expirySeconds));
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
