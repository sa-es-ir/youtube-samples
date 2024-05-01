using EFQueryOptimization.Context;
using EFQueryOptimization.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
    options.EnableSensitiveDataLogging();
    options.LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddScoped<EmployeeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/employees", ([FromServices] EmployeeRepository repository) =>
{
    return repository.GetEmployees();
})
.WithDescription(@"select (name, username, companyName) of top 2 employees 
                    who belongs to Backend or Cloud department 
                    and have at least one Bonus payroll 
                    and part of the company founded in year 2022.")
.WithOpenApi();

app.MapGet("/employees-tunned", ([FromServices] EmployeeRepository repository) =>
{
    return repository.GetEmployees_Tunned();
})
.WithOpenApi();

app.Run();