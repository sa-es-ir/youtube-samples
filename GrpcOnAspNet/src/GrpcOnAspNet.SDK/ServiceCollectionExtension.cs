using GrpcOnAspNet.Server;
using Microsoft.Extensions.DependencyInjection;

namespace GrpcOnAspNet.SDK;

public static class ServiceCollectionExtension
{
    public static void AddGrpcSdk(this IServiceCollection services)
    {
        services.AddGrpcClient<Greeter.GreeterClient>(client =>
        {
            client.Address = new Uri("https://localhost:7251");
        });

        services.AddScoped<IGreeterGrpcService, GreeterGrpcService>();
    }
}
