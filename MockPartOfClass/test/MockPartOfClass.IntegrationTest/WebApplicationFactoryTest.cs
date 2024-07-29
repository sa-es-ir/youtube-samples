using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MockPartOfClass.Models;
using MockPartOfClass.Services;
using NSubstitute;

namespace MockPartOfClass.IntegrationTest;

public class WebApplicationFactoryTest<TProgram> : WebApplicationFactory<TProgram> where TProgram : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var scope = services.BuildServiceProvider().CreateScope();

            var userService = Substitute.ForPartsOf<UserService>(scope.ServiceProvider.GetRequiredService<IHttpClientFactory>());

            userService.GetUserFromGithubAsync(Arg.Any<string>()).Returns(new GithubUserDto
            {
                Id = 1,
                Followers = 100
            });

            services.AddScoped(_ => userService);
        });
    }
}
