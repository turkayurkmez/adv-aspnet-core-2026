namespace usingFilters.Services
{
    public interface IProductService
    {
        bool IsExists(int id);

    }

    public class ProductService : IProductService
    {
        private List<Product> products = new List<Product>()
        {
            new Product{ Id=1, Name ="Ürün A" },
            new Product{ Id=2, Name ="Ürün B" },
            new Product{ Id=3, Name ="Ürün C" },


        };
        public bool IsExists(int id)
        {
            return products.Any(p => p.Id == id);   
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
