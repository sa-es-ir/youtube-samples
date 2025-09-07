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
        var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        tokenSource.CancelAfter(TimeSpan.FromSeconds(10));
        return await userRepository.GetListByQueryAsync(cancellationToken);
    }
}