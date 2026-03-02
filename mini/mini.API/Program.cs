using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using mini.API.Enpoints;
using mini.API.REPR;
using mini.Application;
using mini.Application.Contracts;
using mini.Application.DataTransferObjects;
using mini.Application.Features.Products.Commands.CreateNewProduct;
using mini.Application.Features.Products.Queries.GetAllProducts;
using mini.Application.Features.Products.Queries.GetProductById;
using mini.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddEndpoints(typeof(Program).Assembly);



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

app.MapEndpoints();

//app.MapGet("/getProducts", async (IMediator mediator) =>
//{
//    //var request = new GetAllProductRequest();
//    //var repository = new ProductRepository(new mini.Infrastructure.Data.SampleProductsDbContext(
//    //    new DbContextOptionsBuilder<mini.Infrastructure.Data.SampleProductsDbContext>()
//    //        .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
//    //        .Options
//    //));
//    //var handler = new GetAllProductsRequestHandler(repository);
//    //var response = await handler.Handle(request);    //return Results.Ok(response);

//    var response = await mediator.Send(new GetAllProductRequest());
//    return Results.Ok(response);
//}).Produces<Ok<GetAllProductResponse>>(200);


//app.MapGet("/getProduct/{id}", async (int id, IMediator mediator) =>
//{
//    var response = await mediator.Send(new GetProductByIdRequest(id));
//    return (response is null) ? Results.NotFound() : TypedResults.Ok(response);
//}).Produces<Ok<GetProductByIdResponse>>(200)
//  .Produces<NotFound>(404);

////explicit dönüþ tipli endpoint:

//app.MapPost("/createProduct", async (CreateNewProductRequest request, IMediator mediator) =>
//{
//    var response = await mediator.Send(request);
//    return TypedResults.Created($"/getProduct/{response.Id}", response);
//});

//app.MapProductsEndpoints();




app.Run();

