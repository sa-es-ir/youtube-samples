using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using TemplateMethodPattern.PizzaMaker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeyedScoped<IPizza, PepperoniPizza>(PizzaType.Pepperoni);
builder.Services.AddKeyedScoped<IPizza, MargheritaPizza>(PizzaType.Margherita);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/pizza/{type}", ([FromRoute] PizzaType type, [FromServices] IServiceScopeFactory scopeFactory) =>
{
    using var scope = scopeFactory.CreateScope();
    var pizza = scope.ServiceProvider.GetRequiredKeyedService<IPizza>(type);

    return pizza.MakePizza();
})
.WithName("MakePizza")
.WithOpenApi();

app.Run();
