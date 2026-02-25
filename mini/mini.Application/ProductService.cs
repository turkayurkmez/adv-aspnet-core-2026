using mini.Application.Contracts;
using mini.Application.DataTransferObjects;
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

        public async Task<int> CreateNewProduct(CreateNewProductRequest request)
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

        public Task<int> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductsAsync()
        {
            var products = new List<Product>()
            {
                new Product { Id=1, Name="Ürün A", Description="Açıklama", Price=1 },
                 new Product { Id=2, Name="Ürün B", Description="Açıklama", Price=1 },
            };

            return Task.FromResult(products);

        }

        public Task<int> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
