using HostedServiceAndBackgroundService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<NotifyHostedService>();

builder.Services.Configure<HostOptions>(x =>
{
    x.ServicesStartConcurrently = true;
    x.ServicesStopConcurrently = true;
    x.StartupTimeout = TimeSpan.FromSeconds(10);
    x.ShutdownTimeout = TimeSpan.FromSeconds(10);
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
