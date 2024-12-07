using CITUCrate.Models;

namespace CITUCrate.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByNameAsync(string name); // For checking duplicates
        Task AddProductAsync(Product product); // For adding a new product
    }

}
