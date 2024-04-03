using CustomMediatR;
using CustomMediatR.Commands;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMyMediator();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/user-command", async ([FromServices] IMyMediator meditor, CancellationToken cancellationToken) =>
{
    var response = await meditor.Send(new UserCommand("Mediator"), cancellationToken);

    response = await meditor.Send(new UserUpdateCommand("UpdatedMediator"), cancellationToken);

    return response;
})
.WithName("UserCommand")
.WithOpenApi();


app.Run();