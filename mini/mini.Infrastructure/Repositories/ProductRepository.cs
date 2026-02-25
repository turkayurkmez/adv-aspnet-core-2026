using Microsoft.EntityFrameworkCore;
using mini.Application.Contracts;
using mini.Domain;
using mini.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace mini.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly SampleProductsDbContext dbContext;

        //constructor injection of the dbContext
        public ProductRepository(SampleProductsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task Create(Product entity)
        {
            await dbContext.Products.AddAsync(entity);
            await dbContext.SaveChangesAsync();


        }

        public async Task Delete(int id)
        {
            await dbContext.Products.Where(p => p.Id == id).ExecuteDeleteAsync();

        }

        public async Task<Product> Get(int id)
        {
           return await dbContext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetAll()
        {
          return await dbContext.Products.ToListAsync();
        }

        public async Task<List<Product>> SearchByName(string name)
        {
           return await dbContext.Products.Where(p => p.Name.Contains(name)).ToListAsync();
        }

        public async Task Update(Product entity)
        {
            dbContext.Products.Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
