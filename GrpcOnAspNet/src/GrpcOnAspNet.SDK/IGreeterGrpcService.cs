using Grpc.Core;
using GrpcOnAspNet.Server;

namespace GrpcOnAspNet.SDK;

public interface IGreeterGrpcService
{
    Task<string> SayHelloAsync(string name, CancellationToken cancellationToken);
}


public class GreeterGrpcService : IGreeterGrpcService
{
    private readonly Greeter.GreeterClient grpcClient;

    public GreeterGrpcService(Greeter.GreeterClient grpcClient)
    {
        this.grpcClient = grpcClient;
    }

    public async Task<string> SayHelloAsync(string name, CancellationToken cancellationToken)
    {
        try
        {
            var result = await grpcClient.SayHelloAsync(new HelloRequest { Name = name }, cancellationToken: cancellationToken);

            return result.Message;
        }
        catch (RpcException ex)
        {
            throw;
        }

    }
}