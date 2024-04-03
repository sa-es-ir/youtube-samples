using CustomMediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/user-command", async ([FromServices] IMediator meditor, CancellationToken cancellationToken) =>
{
    var response = await meditor.Send(new UserCommand("Mediator"), cancellationToken);

    return response;
})
.WithName("UserCommand")
.WithOpenApi();

app.Run();

