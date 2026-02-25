using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace mini.Application.Features.Products.Commands.CreateNewProduct
{
    public record CreateNewProductRequest(
        string Name,
        string Description,
        decimal Price
    ) : IRequest<CreateNewProductResponse>;

    public record CreateNewProductResponse(int Id);

}
