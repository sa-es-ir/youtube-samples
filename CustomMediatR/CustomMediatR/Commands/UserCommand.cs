using MediatR;

namespace CustomMediatR.Commands;

public record UserCommand(string? Name) : IRequest<UserResponse>;

public record UserResponse(string? Message);
