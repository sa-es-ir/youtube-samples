using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using RoleBasedAuthorization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepositoy, UserRepository>();

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents()
        {
            OnTokenValidated = async context =>
            {
                var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepositoy>();

                var userRoles = await userRepository.GetUserRolesAsync(context.Principal!.Identity!.Name, context.HttpContext.RequestAborted);

                var userClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role));

                ((ClaimsIdentity)context.Principal!.Identity!).AddClaims(userClaims);
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/users-minimal", [Authorize(Roles = "Admin")] () =>
{
    var users = new[]
         {
            new { Id = 1, Name = "John" },
            new { Id = 2, Name = "Jane" },
            new { Id = 3, Name = "Jack" }
        };

    return users;
})
.WithOpenApi();

app.Run();