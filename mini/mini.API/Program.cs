using MediatR;
using Microsoft.EntityFrameworkCore;
using mini.Application;
using mini.Application.Contracts;
using mini.Application.DataTransferObjects;
using mini.Application.Features.Products.Commands.CreateNewProduct;
using mini.Application.Features.Products.Queries.GetAllProducts;
using mini.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


//SQLite için dbContext'i ekleyelim:

builder.Services.AddDbContext<mini.Infrastructure.Data.SampleProductsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllProductsRequestHandler>());


var app = builder.Build();


// Ensure the database is created and apply migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<mini.Infrastructure.Data.SampleProductsDbContext>();
    dbContext.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/getProducts", async (IMediator mediator) =>
{
    //var request = new GetAllProductRequest();
    //var repository = new ProductRepository(new mini.Infrastructure.Data.SampleProductsDbContext(
    //    new DbContextOptionsBuilder<mini.Infrastructure.Data.SampleProductsDbContext>()
    //        .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
    //        .Options
    //));
    //var handler = new GetAllProductsRequestHandler(repository);
    //var response = await handler.Handle(request);
    //return Results.Ok(response);

    var response = await mediator.Send(new GetAllProductRequest());
    return Results.Ok(response);
});

app.MapPost("/createProduct", async (CreateNewProductRequest request, IMediator mediator) =>
{
    var response = await mediator.Send(request);
    return Results.Ok(response);
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
