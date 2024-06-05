var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAuthentication()
    .AddJwtBearer();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserGroup", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("user-group",
            allowedValues: ["FE", "Mobile"]
            );
    });
});

var app = builder.Build();

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
.RequireAuthorization("UserGroup")
.WithOpenApi();

app.Run();
