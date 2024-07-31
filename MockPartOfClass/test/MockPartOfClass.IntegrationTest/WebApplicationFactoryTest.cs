using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MockPartOfClass.Services;
using NSubstitute;

namespace MockPartOfClass.IntegrationTest;

public class WebApplicationFactoryTest<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // setup test services

            var scope = services.BuildServiceProvider().CreateScope();
            var userService = Substitute.ForPartsOf<UserService>(scope.ServiceProvider.GetRequiredService<IHttpClientFactory>());

            userService.GetUserFromGithubAsync(Arg.Any<string>()).Returns(new Models.GithubUserDto
            {
                Id = 1,
                Followers = 100
            });

            services.RemoveAll<IUserService>();

            services.AddScoped<IUserService>(_ => userService);
        });
    }
}