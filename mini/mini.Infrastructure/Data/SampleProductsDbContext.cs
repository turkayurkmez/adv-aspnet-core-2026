using Microsoft.EntityFrameworkCore;
using mini.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace mini.Infrastructure.Data
{
    public class SampleProductsDbContext : DbContext
    {

        public SampleProductsDbContext(DbContextOptions<SampleProductsDbContext> options) : base(options)
        {

        }

        //Product nesnesini Dbset olarak kullan:
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Elektronik kategorisi için 3 örnek ürün ekleyelim:
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "Yüksek performanslı laptop", Price = 1500.00m },
                new Product { Id = 2, Name = "Akıllı Telefon", Description = "Gelişmiş özelliklere sahip akıllı telefon", Price = 800.00m },
                new Product { Id = 3, Name = "Tablet", Description = "Taşınabilir ve güçlü tablet", Price = 600.00m }
            );



        }


    }
}
