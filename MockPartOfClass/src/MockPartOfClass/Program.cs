using Microsoft.AspNetCore.Mvc;
using MockPartOfClass.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpClient("GithubAPI", client =>
{
    client.BaseAddress = new Uri("https://api.github.com/");
    client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
    client.DefaultRequestHeaders.Add("User-Agent", "test");
});

var app = builder.Build();

app.UseHttpsRedirection();


app.MapGet("/users/{username}", async (string username, [FromServices] IUserService userService) =>
{
    return await userService.GetUserAsync(username);
})
.WithName("Users");

app.Run();

public partial class Program { }

