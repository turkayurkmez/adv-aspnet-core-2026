using MediatR;
using mini.Application.Contracts;
using mini.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace mini.Application.Features.Products.Commands.CreateNewProduct
{
    public class CreateNewProductHandler : IRequestHandler<CreateNewProductRequest, CreateNewProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateNewProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CreateNewProductResponse> Handle(CreateNewProductRequest request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price

            };
            await _productRepository.Create(product);



            return new CreateNewProductResponse(product.Id);

        }
           
    }
}
