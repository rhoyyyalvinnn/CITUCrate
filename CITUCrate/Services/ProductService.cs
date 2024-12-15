using CITUCrate.DTO;
using CITUCrate.Models;
using CITUCrate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CITUCrate.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDTO>> GetAllProductDTOsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            return products.Select(p => new ProductDTO
            {
                ID = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                Quantity = p.Quantity,
                ShortDescription = p.ShortDescription,
                ImageUrl = p.ImageUrl,
                TotalSales = p.TotalSales,
            }).ToList();
        }

        public async Task<bool> AddProductAsync(AddUpdateProductDTO addProductDto, IFormFile imageFile)
        {
            var existingProduct = await _productRepository.GetProductByNameAsync(addProductDto.Name);
            if (existingProduct != null)
            {
                return false;
            }

            string imageUrl = "/images/default.jpg";
            if (imageFile != null && imageFile.Length > 0)
            {
                var productImagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "productimages");
                if (!Directory.Exists(productImagesFolder))
                {
                    Directory.CreateDirectory(productImagesFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
                var filePath = Path.Combine(productImagesFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                imageUrl = $"/images/productimages/{uniqueFileName}";
            }

            var newProduct = new Product
            {
                Name = addProductDto.Name,
                Category = addProductDto.Category,
                Price = addProductDto.Price,
                Quantity = addProductDto.Quantity,
                ShortDescription = addProductDto.ShortDescription,
                ImageUrl = imageUrl,
                TotalSales = 0
            };

            await _productRepository.AddProductAsync(newProduct);
            return true;
        }


        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) return null;

            var productDto = new ProductDTO
            {
                ID = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
                Quantity = product.Quantity,
                ShortDescription = product.ShortDescription,
                ImageUrl = product.ImageUrl,
                TotalSales = product.TotalSales,
            };

            return productDto;
        }

        public async Task<bool> UpdateProductAsync(int productId, AddUpdateProductDTO updateProductDTO, IFormFile imageFile)
        {

            var existingProduct = await _productRepository.GetProductByIdAsync(productId);
            if (existingProduct == null)
            {
                throw new Exception($"Product with ID {productId} not found.");
            }

            // Handle image update if a new image is provided
            if (imageFile != null && imageFile.Length > 0)
            {
                var productImagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "productimages");
                if (!Directory.Exists(productImagesFolder))
                {
                    Directory.CreateDirectory(productImagesFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
                var filePath = Path.Combine(productImagesFolder, uniqueFileName);

                // Save the new image file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Update image URL to the newly uploaded file
                existingProduct.ImageUrl = $"/images/productimages/{uniqueFileName}";
            }

            // Update the other product properties with the new values from the DTO
            existingProduct.Name = updateProductDTO.Name;
            existingProduct.Category = updateProductDTO.Category;
            existingProduct.Price = updateProductDTO.Price;
            existingProduct.Quantity = updateProductDTO.Quantity;
            existingProduct.ShortDescription = updateProductDTO.ShortDescription;

            // Save the updated product to the repository
            await _productRepository.UpdateProductAsync(existingProduct);
            return true;
        }

        // Delete a product
        public async Task<bool> DeleteProductAsync(int productId)
        {
            return await _productRepository.DeleteProductAsync(productId);
        }

        //Get products based on category
        public async Task<List<ProductDTO>> GetProductsByCategoryAsync(string category)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(category);

            return products.Select(p => new ProductDTO
            {
                ID = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                Quantity = p.Quantity,
                ShortDescription = p.ShortDescription,
                ImageUrl = p.ImageUrl
            }).ToList();
        }

    }
}

