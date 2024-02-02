namespace MultiLayerCache.CacheProviders;

public interface ICacheProvider
{
    Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> getFromDbFunction, TimeSpan expiry, CancellationToken cancellationToken);

    Task SaveAsync<T>(string key, T value, TimeSpan expiry, CancellationToken cancellationToken);

    Task<T?> GetAsync<T>(string key);

    Task<bool> DeleteAsync(string key, CancellationToken cancellationToken);
}
