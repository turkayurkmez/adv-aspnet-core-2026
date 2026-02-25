using mini.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace mini.Application.Contracts
{

    public interface IRepository<T>
    {
        Task Create(T entity)   ;
        Task Update(T entity) ;
        Task Delete(int id) ;
        Task<T> Get(int id) ;
        Task<List<T>> GetAll() ;

    }
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> SearchByName(string name);
    }
}
