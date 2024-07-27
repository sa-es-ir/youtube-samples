using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using TemplateMethodPattern.PizzaMaker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPizza, MargheritaPizza>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseHttpsRedirection();


app.MapPost("/pizza", async ([FromServices] IPizza pizza) =>
{
    await pizza.MakePizza();

    return "Pizza is ready!";
})
.WithName("MakePizza");

app.Run();
