namespace MockPartOfClass.Models;

public class UserDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public int GithubFollowers { get; set; }
    public long GithubUserId { get; set; }
}
