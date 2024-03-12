using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantDbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));

    options.LogTo(Console.WriteLine, LogLevel.Information);
    options.EnableSensitiveDataLogging();
});

builder.Services.AddScoped<TenantProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<TenantIdentifierMiddleware>();

app.MapGet("/students", async ([FromServices] AppDbContext context, [FromServices] TenantProvider tenantProvider, CancellationToken cancellationToken) =>
{
    List<Student> students;

    if (tenantProvider.TenantId is null)
        students = await context.GetAllStudentsAsync(cancellationToken);
    else
        students = await context.GetStudentsAsync(cancellationToken);

    return students;
})
.WithName("Students")
.WithOpenApi();

app.Run();
