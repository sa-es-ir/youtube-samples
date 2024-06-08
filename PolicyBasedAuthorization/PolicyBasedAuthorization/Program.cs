using Microsoft.AspNetCore.Authentication;
using PolicyBasedAuthorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformer>();

builder.Services.AddAuthentication()
    .AddJwtBearer();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AuthConstants.UserGroupWeb, policy =>
    {
        policy.RequireClaim(AuthConstants.UserGroupClaim, AuthConstants.WebClaim);
    });

    options.AddPolicy(AuthConstants.UserGroupMobile, policy =>
    {
        policy.RequireRole("guest");
        policy.RequireClaim(AuthConstants.UserGroupClaim, AuthConstants.MobileClaim);
    });
});

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
.RequireAuthorization(AuthConstants.UserGroupWeb);

app.Run();
