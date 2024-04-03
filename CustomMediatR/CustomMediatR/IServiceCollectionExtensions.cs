using CustomMediatR.CommandHandlers;
using CustomMediatR.Commands;

namespace CustomMediatR
{
    public static class IServiceCollectionExtensions
    {
        public static void AddMyMediator(this IServiceCollection services)
        {
            services.AddTransient<IMyRequestHandler<UserCommand, UserResponse>, UserCommandHandler>();
            services.AddTransient<IMyRequestHandler<UserUpdateCommand, UserResponse>, UserUpdateCommandHandler>();

            services.AddScoped<IMyMediator, MyMediator>();
        }
    }
}
