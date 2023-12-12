using OpenIdConnectAuthentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthenticationService(builder.Configuration);

//builder.Services.AddScoped<CustomAuthMiddleware>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<CustomAuthMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
