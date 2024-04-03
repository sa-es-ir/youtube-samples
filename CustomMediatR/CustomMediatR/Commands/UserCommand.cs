namespace CustomMediatR.Commands;

public record UserCommand(string? Name) : IMyRequest<UserResponse>;

public record UserResponse(string? Message);

public record UserUpdateCommand(string? Name) : IMyRequest<UserResponse>;