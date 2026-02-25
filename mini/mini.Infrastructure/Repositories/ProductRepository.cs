using mini.Application.Contracts;
using mini.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace mini.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private List<Product> products=new List<Product>();
        public Task Create(Product entity)
        {
            products.Add(entity);

            return Task.CompletedTask;
        
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> SearchByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
