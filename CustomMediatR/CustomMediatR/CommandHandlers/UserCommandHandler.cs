using CustomMediatR.Commands;

namespace CustomMediatR.CommandHandlers;

public class UserCommandHandler : IMyRequestHandler<UserCommand, UserResponse>
{
    private readonly ILogger<UserCommandHandler> logger;

    public UserCommandHandler(ILogger<UserCommandHandler> logger)
    {
        this.logger = logger;
    }

    public Task<UserResponse> Handle(UserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Trying to run the user command...");

        return Task.FromResult(new UserResponse($"Hello User: {request.Name}"));
    }
}


public class UserUpdateCommandHandler : IMyRequestHandler<UserUpdateCommand, UserResponse>
{
    private readonly ILogger<UserUpdateCommandHandler> logger;

    public UserUpdateCommandHandler(ILogger<UserUpdateCommandHandler> logger)
    {
        this.logger = logger;
    }

    public Task<UserResponse> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Trying to run the user update command...");

        return Task.FromResult(new UserResponse($"Hello  User update: {request.Name}"));
    }
}