using MockPartOfClass.Models;

namespace MockPartOfClass.Services;

public interface IUserService
{
    Task<UserDto> GetUserAsync(string username);
}

public class UserService : IUserService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UserService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<UserDto> GetUserAsync(string username)
    {
        var dbUser = await GetUserFromDbAsync(username);

        var githubUser = await GetUserFromGithubAsync(username);

        if (githubUser is not null)
        {
            dbUser.GithubFollowers = githubUser.Followers;
            dbUser.GithubUserId = githubUser.Id;
        }

        return dbUser;

    }

    private Task<UserDto> GetUserFromDbAsync(string username)
    {
        return Task.FromResult(new UserDto
        {
            Name = "Saeed Esmaeelinejad",
            Email = "my.test.email@gmail.com"
        });
    }

    public virtual async Task<GithubUserDto?> GetUserFromGithubAsync(string username)
    {
        var httpClient = _httpClientFactory.CreateClient("GithubAPI");
        var response = await httpClient.GetAsync($"users/{username}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<GithubUserDto>();
    }
}
