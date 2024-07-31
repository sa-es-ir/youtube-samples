using FluentAssertions;
using MockPartOfClass.Models;
using System.Net;
using System.Net.Http.Json;

namespace MockPartOfClass.IntegrationTest;

public class UserApiTest : IClassFixture<WebApplicationFactoryTest<Program>>
{
    private readonly HttpClient _httpClient;

    public UserApiTest(WebApplicationFactoryTest<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task GetUser_ValidUsername_ReturnsOk()
    {
        var username = "sa-es-ir";
        var response = await _httpClient.GetAsync($"/users/{username}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var user = await response.Content.ReadFromJsonAsync<UserDto>();

        user!.Name.Should().Be("Saeed Esmaeelinejad");
        user.GithubFollowers.Should().BeGreaterThan(0);
    }
}