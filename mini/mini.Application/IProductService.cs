using mini.Application.DataTransferObjects;
using mini.Application.Features.Products.Commands.CreateNewProduct;
using mini.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace mini.Application
{
    public interface IProductService
    {
        Task<int> CreateNewProduct(CreateNewProductRequestDto product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task<Product> GetProductById(int id);

        Task<List<Product>> GetProductsAsync();

        //Ey yazılımcı, bir işlev eklemek istediğinde, bu arayüze yeni bir metot eklemelisin!

        //Bunun yerine; her yeni işlev için ayrı bir class oluşturup, o class'ların tek bir işlevi yerine getirmesini sağlayabilirsin. Bu, yönteme Vertical Slicing denir. Yani, her yeni işlev için ayrı bir class oluşturup, o class'ların tek bir işlevi yerine getirmesini sağlayabilirsin.


    }
}
