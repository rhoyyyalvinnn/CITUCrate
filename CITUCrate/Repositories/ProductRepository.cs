using CITUCrate.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CITUCrate.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly UserContext _context;

        public ProductRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
