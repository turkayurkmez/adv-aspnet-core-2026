using MediatR;
using mini.Application.Features.Products.Commands.CreateNewProduct;

namespace mini.API.REPR
{
    public class CreateNewProduct : IEndpoint
    {
        public void MapEndpoint(RouteGroupBuilder group)
        {
            group.MapPost("/create", handle())
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .ProducesProblem(StatusCodes.Status500InternalServerError)
               .WithName("CreateNewProduct")
               .WithSummary("Yeni bir ürün oluşturur.");
        }
        static Func<CreateNewProductRequest, IMediator, Task<IResult>> handle()
        {
            return async (CreateNewProductRequest request, IMediator mediator) =>
            {
                var response = await mediator.Send(request);
                return TypedResults.Created($"/getProduct/{response.Id}", response);
            };
        }

    }
}
