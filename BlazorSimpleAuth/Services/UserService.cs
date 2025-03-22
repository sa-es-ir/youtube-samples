namespace BlazorSimpleAuth.Services;

public class UserService
{
    public User? GetUser(string username)
    {
        if (username != "admin")
            return default;

        return new User
        {
            Name = "admin",
            Password = "admin"
        };
    }
}
public class User
{
    public required string Name { get; set; }

    public required string Password { get; set; }
}
