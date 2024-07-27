using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using TemplateMethodPattern.PizzaMaker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeyedScoped<IPizza, MargheritaPizza>(PizzaType.Margherita);
builder.Services.AddKeyedScoped<IPizza, PepperoniPizza>(PizzaType.Pepperoni);

builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/pizza/{type}", async ([FromRoute] PizzaType type, [FromServices] IServiceScopeFactory scopeFactory) =>
{
    var scope = scopeFactory.CreateScope();

    var pizza = scope.ServiceProvider.GetRequiredKeyedService<IPizza>(type);

    await pizza.MakePizza();

    return "Pizza is ready!";
})
.WithName("MakePizza");

app.Run();
