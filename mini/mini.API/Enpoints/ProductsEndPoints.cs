using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using mini.Application.Features.Products.Commands.CreateNewProduct;
using mini.Application.Features.Products.Queries.GetAllProducts;
using mini.Application.Features.Products.Queries.GetProductById;

namespace mini.API.Enpoints
{
    public static class ProductsEndPoints
    {
        /*
         * Program.cs üzerindeki kalabalığı azaltmak ve kodun okunabilirliğini artırmak için ürünlerle ilgili endpointleri bu sınıfa (extension metoda)taşıdık. Program.cs içerisinde sadece bu sınıfın MapProductsEndpoints metodunu çağırarak ürünlerle ilgili tüm endpointleri ekleyebiliriz.
         */
        public static void MapProductsEndpoints(this WebApplication app)
        {
            app.MapGet("/products/{id}", async (int id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductByIdRequest(id));
                return Results.Ok(response);
            });

            app.MapPost("/createProduct", async (CreateNewProductRequest request, IMediator mediator) =>
            {
                var response = await mediator.Send(request);
                return TypedResults.Created($"/getProduct/{response.Id}", response);
            });

            app.MapGet("/getProducts", async (IMediator mediator) =>
            {
                //var request = new GetAllProductRequest();
                //var repository = new ProductRepository(new mini.Infrastructure.Data.SampleProductsDbContext(
                //    new DbContextOptionsBuilder<mini.Infrastructure.Data.SampleProductsDbContext>()
                //        .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
                //        .Options
                //));
                //var handler = new GetAllProductsRequestHandler(repository);
                //var response = await handler.Handle(request);    //return Results.Ok(response);

                var response = await mediator.Send(new GetAllProductRequest());
                return Results.Ok(response);
            }).Produces<Ok<GetAllProductResponse>>(200);
        }
    }
}
