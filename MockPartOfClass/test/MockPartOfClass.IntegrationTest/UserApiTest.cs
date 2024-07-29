using FluentAssertions;
using MockPartOfClass.Models;
using System.Net;
using System.Net.Http.Json;

namespace MockPartOfClass.IntegrationTest;

public class UserApiTest : IClassFixture<WebApplicationFactoryTest<Program>>
{
    private readonly WebApplicationFactoryTest<Program> _factory;

    public UserApiTest(WebApplicationFactoryTest<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetUser_ValidUsername_ReturnsOk()
    {
        var httpClient = _factory.CreateClient();

        var response = await httpClient.GetAsync("/users/sa-es-ir");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var user = await response.Content.ReadFromJsonAsync<UserDto>();

        user!.Name.Should().Be("Saeed Esmaeelinejad");
        user.GithubFollowers.Should().BeGreaterThan(0);
    }
}