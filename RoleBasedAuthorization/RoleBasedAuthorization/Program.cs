using Microsoft.AspNetCore.Authorization;
using RoleBasedAuthorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepositoy, UserRepository>();

builder.Services.AddAuthentication()
    .AddJwtBearer();

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


app.MapGet("/users-minimal", () =>
{
    var users = new[]
         {
            new { Id = 1, Name = "John" },
            new { Id = 2, Name = "Jane" },
            new { Id = 3, Name = "Jack" }
        };

    return users;
})
.RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
.WithOpenApi();

app.Run();