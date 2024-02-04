using StackExchange.Redis;

namespace MultiLayerCache.CacheProviders;

public static class ServiceCollectionExtension
{
    public static void AddCacheServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();

        services.AddScoped<ICacheProvider, InMemoryCacheProvider>();
        services.AddScoped<ICacheProvider, RedisCacheProvider>();

        services.AddScoped<CacheManager>();

        var redisConnection = configuration.GetConnectionString("Redis");

        var redisOptions = ConfigurationOptions.Parse(redisConnection!);

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisOptions));

        services.AddScoped<IDatabase>(provider => provider.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
    }
}
