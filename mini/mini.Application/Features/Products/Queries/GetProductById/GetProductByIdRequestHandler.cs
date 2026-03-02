using MediatR;
using mini.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace mini.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdRequestHandler : IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdRequestHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.Id);
            return new GetProductByIdResponse(product.Id, product.Name, product.Price, product.Description);

        }
    }
}
