using mini.Application.Contracts;
using mini.Application.DataTransferObjects;
using mini.Application.Features.Products.Commands.CreateNewProduct;
using mini.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace mini.Application
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> CreateNewProduct(CreateNewProductRequestDto request)
        {
            var product = new Product
            {
                Description = request.Description,
                Name = request.Name,
                Price = request.Price
            };

            await _productRepository.Create(product);

            return product.Id;
        }



        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.Get(id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _productRepository.GetAll();


        }


        public async Task DeleteProduct(int id)
        {
            await _productRepository.Delete(id);

        }

        public async Task UpdateProduct(Product product)
        {
            await _productRepository.Update(product);


        }
    }
}
