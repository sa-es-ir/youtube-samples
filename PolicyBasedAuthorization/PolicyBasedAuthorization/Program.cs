var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthentication()
    .AddJwtBearer();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/products-minimal", () =>
{
    var products = new[]
          {
            new { Id = 1, Name = "Laptop" },
            new { Id = 2, Name = "Mouse" },
            new { Id = 3, Name = "Keyboard" }
        };

    return products;
})
.RequireAuthorization();

app.Run();
