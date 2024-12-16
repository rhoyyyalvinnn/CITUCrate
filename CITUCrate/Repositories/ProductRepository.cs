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



nn = new NeuralNet(2, 2, 1);

for (int x = 0; x < 100; x++)
{
    nn.setInputs(0, 0.0);
    nn.setInputs(1, 0.0);
    nn.setDesiredOutput(0, 0.0);
    nn.learn();

    nn.setInputs(0, 0.0);
    nn.setInputs(1, 1.0);
    nn.setDesiredOutput(0, 1.0);
    nn.learn();

    nn.setInputs(0, 1.0);
    nn.setInputs(1, 0.0);
    nn.setDesiredOutput(0, 1.0);
    nn.learn();

    nn.setInputs(0, 1.0);
    nn.setInputs(1, 1.0);
    nn.setDesiredOutput(0, 1.0);
    nn.learn();
}

nn.setInputs(0, Convert.ToDouble(textbox1.Text));
nn.setInputs(1, Convert.ToDouble(textbox2.Text));
nn.run();
textbox3.Text = "" + nn.getOutputData(0);