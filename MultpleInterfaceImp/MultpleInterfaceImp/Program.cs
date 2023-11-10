using MultpleInterfaceImp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<FooService>();
builder.Services.AddScoped<BarService>();

builder.Services.AddScoped<StrategyFooService>(provider => (type) =>
{
    switch (type)
    {
        case FooType.Foo: return provider.GetRequiredService<FooService>();
        case FooType.Bar: return provider.GetRequiredService<BarService>();
        default: throw new NotImplementedException();
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
