namespace CustomMediatR;

public interface IMyRequest<out TResponse> { };

public interface IMyRequestHandler<in TRequest, TResponse>
    where TRequest : IMyRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}


public interface IMyMediator
{
    Task<TResponse> Send<TResponse>(IMyRequest<TResponse> request, CancellationToken cancellationToken = default);
}


public class MyMediator : IMyMediator
{
    private readonly IServiceProvider serviceProvider;

    public MyMediator(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(IMyRequest<TResponse> request, CancellationToken cancellationToken)
    {
        var types = new Type[] { request.GetType(), typeof(TResponse) };
        var genericBase = typeof(RequestHandlerDecoratorImplementation<,>);

        var myHandlerType = genericBase.MakeGenericType(types);

        var myHandlerInstance = (RequestHandlerDecorator<TResponse>?)Activator.CreateInstance(myHandlerType);

        return await myHandlerInstance!.Handle(request, serviceProvider, cancellationToken);
    }
}

public class RequestHandlerDecoratorImplementation<TRequest, TResponse> : RequestHandlerDecorator<TResponse>
    where TRequest : IMyRequest<TResponse>
{
    public override Task<TResponse> Handle(IMyRequest<TResponse> request, IServiceProvider serviceProvider,
    CancellationToken cancellationToken)
    {
        return serviceProvider.GetRequiredService<IMyRequestHandler<TRequest, TResponse>>()
            .Handle((TRequest)request, cancellationToken);
    }
}

public abstract class RequestHandlerDecorator<TResponse>
{
    public abstract Task<TResponse> Handle(IMyRequest<TResponse> request, IServiceProvider serviceProvider,
        CancellationToken cancellationToken);
}