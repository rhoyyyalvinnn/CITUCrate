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

        public async Task<Product> GetProductByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }




        public async Task<int> GetProductIdByNameAsync(string name)
        {
            var product = await _context.Products
                .Where(p => p.Name == name)
                .FirstOrDefaultAsync();

            return product.Id;
        }
    }
}
