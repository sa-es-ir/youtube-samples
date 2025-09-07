using Microsoft.EntityFrameworkCore;

namespace EFCoreCancellationToken.Infrastructure;

public interface IUserRepository
{
    Task<List<User>> GetListAsync(CancellationToken cancellationToken);
    Task<List<User>> GetListByQueryAsync(CancellationToken cancellationToken);
}

public class UserRepository(SampleDbContext context) : IUserRepository
{
    public async Task<List<User>> GetListAsync(CancellationToken cancellationToken)
    {
        return await context.Users.ToListAsync(cancellationToken);
    }

    public async Task<List<User>> GetListByQueryAsync(CancellationToken cancellationToken)
    {
        return await context.Database.SqlQuery<User>($"WAITFOR DELAY '00:00:05';SELECT * FROM Users")
            .ToListAsync(cancellationToken);
    }
}
