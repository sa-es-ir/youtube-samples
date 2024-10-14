using KafkaWithBlockingCollection;
using KafkaWithBlockingCollection.Brokers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMessageBroker, MessageBroker>();
builder.Services.AddHostedService<ConsumerBackgroundService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/produce-message", (IMessageBroker messageBroker) =>
{
    var message = new MessageModel
    {
        Id = Guid.NewGuid(),
        Name = $"{Constants.TOPIC}__{Guid.NewGuid()}",
        Description = $"A description for {Constants.TOPIC}"
    };

    messageBroker.Produce(Constants.TOPIC, message);
    return message;
})
.WithName("ProduceMessage")
.WithOpenApi();

app.Run();
