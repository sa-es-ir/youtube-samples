using KafkaDotNet.Consumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Start consuming events ...");

var builder = Host.CreateApplicationBuilder();

builder.Services.AddHostedService<EventConsumerJob>();

builder.Build().Run();