using MediatR;
using mini.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace mini.Application.Features.Products.Queries.GetAllProducts
{

    //analoji: Handler bir hava alanı pisti:
    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductRequest, GetAllProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsRequestHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetAllProductResponse> Handle(GetAllProductRequest request)
        {
            var products = await _productRepository.GetAll();

            var productDtos = products.Select(p => new ProductDisplayDto(
                p.Id,
                p.Name,
                p.Description,
                p.Price
            )).ToList();

            return new GetAllProductResponse(productDtos);
        }

        public async Task<GetAllProductResponse> Handle(GetAllProductRequest request, CancellationToken cancellationToken)
        {

            var products = await _productRepository.GetAll();

            var productDtos = products.Select(p => new ProductDisplayDto(
                p.Id,
                p.Name,
                p.Description,
                p.Price
            )).ToList();

            return new GetAllProductResponse(productDtos);
        }
    }
}
