using mini.Application.DataTransferObjects;
using mini.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace mini.Application
{
    public interface IProductService
    {
        Task<int> CreateNewProduct(CreateNewProductRequest product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task<Product> GetProductById(int id);

        Task<List<Product>> GetProductsAsync();
    }
}
