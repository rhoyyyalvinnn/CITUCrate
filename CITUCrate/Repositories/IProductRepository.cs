﻿using CITUCrate.DTO;
using CITUCrate.Models;

namespace CITUCrate.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByNameAsync(string name); // For checking duplicates
        Task AddProductAsync(Product product); // For adding a new product

        Task<Product> GetProductByIdAsync(int id);
        Task UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int productId);
        Task<int> GetProductIdByNameAsync(string name);
        Task<List<Product>> GetProductsByCategoryAsync(string category);
    }

}
