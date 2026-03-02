using MediatR;
using mini.Application.Features.Products.Queries.GetProductById;

namespace mini.API.REPR
{
    public class GetProductById : IEndpoint
    {
        public void MapEndpoint(RouteGroupBuilder group)
        {
            group.MapGet("/get/{id}", async (int id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductByIdRequest(id));
                if (response == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(response);
            })
               .ProducesProblem(StatusCodes.Status404NotFound)
               .ProducesProblem(StatusCodes.Status500InternalServerError)
               .WithName("GetProductById")
               .WithSummary("Belirtilen ID'ye sahip ürünü getirir.");
        }


    }
}
