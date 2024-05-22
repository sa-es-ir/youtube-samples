namespace RoleBasedAuthorization;

public interface IUserRepositoy
{
    Task<List<string>> GetUserRolesAsync(string? username, CancellationToken cancellationToken);
}

public class UserRepository : IUserRepositoy
{
    public Task<List<string>> GetUserRolesAsync(string? username, CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<string> { "Admin" });
    }
}
