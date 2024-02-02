using MultiLayerCache;
using MultiLayerCache.CacheProviders;
using MultiLayerCache.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddScoped<WeatherService>();

// Register Cache providers
builder.Services.AddCacheServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Map Api
app.MapWeatherApi();

app.Run();
