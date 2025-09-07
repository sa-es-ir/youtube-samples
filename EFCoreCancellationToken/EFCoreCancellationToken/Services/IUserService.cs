using EFCoreCancellationToken.Infrastructure;

namespace EFCoreCancellationToken.Services;

public interface IUserService
{
    Task<List<User>> GetListAsync(CancellationToken cancellationToken);
}

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<List<User>> GetListAsync(CancellationToken cancellationToken)
    {
        return await userRepository.GetListByQueryAsync(cancellationToken);
    }
}