using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace mini.Application.Features.Products.Queries.GetProductById
{
    public record GetProductByIdRequest(int Id ) : IRequest<GetProductByIdResponse>;

    public record GetProductByIdResponse(int Id, string Name, decimal Price, string Description);

}
