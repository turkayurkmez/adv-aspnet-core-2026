
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using mini.Application.Features.Products.Queries.GetAllProducts;

namespace mini.API.REPR
{
    public class GetAllProducts : IEndpoint
    {


        public void MapEndpoint(RouteGroupBuilder productsGroup)
        {

            

            productsGroup.MapGet("/getAll", handle())
               .Produces<Ok<GetAllProductResponse>>(200)               
               .ProducesProblem(StatusCodes.Status500InternalServerError)
               .WithName("GetAllProducts")
               .WithSummary("Tüm ürünleri getirir.");



        }


        static Func<IMediator, Task<IResult>> handle()
        {
            return async (IMediator mediator) =>
            {

                var response = await mediator.Send(new GetAllProductRequest());
                return Results.Ok(response);
            };
        }

    }
}
