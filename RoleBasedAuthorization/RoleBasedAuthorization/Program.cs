using Microsoft.AspNetCore.Authorization;
using RoleBasedAuthorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepositoy, UserRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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