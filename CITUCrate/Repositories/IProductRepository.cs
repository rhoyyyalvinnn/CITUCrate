using CITUCrate.Models;

namespace CITUCrate.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
    }

}
