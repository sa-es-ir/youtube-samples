
namespace MutliLayerCache.CacheProviders;

public class RedisCacheProvider : ICacheProvider
{
    public Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> getFromDbFunction, TimeSpan expiry, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string key, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync<T>(string key, T value, TimeSpan expiry, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetAsync<T>(string key)
    {
        throw new NotImplementedException();
    }
}
