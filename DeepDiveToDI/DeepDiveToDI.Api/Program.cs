using DeepDiveToDI.Domain;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<ISingletonService, SingletonService>();
builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddTransient<ITransientService, TransientService>();

builder.Services.AddSingleton(new SingletonService());

builder.Services.AddKeyedScoped<IEmailService, GoogleEmailService>("Google");
builder.Services.AddKeyedScoped<IEmailService, YahooEmailService>("Yahoo");

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
