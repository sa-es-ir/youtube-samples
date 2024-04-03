using CustomMediatR.Commands;
using MediatR;

namespace CustomMediatR.CommandHandlers;

public class UserCommandHandler : IRequestHandler<UserCommand, UserResponse>
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
