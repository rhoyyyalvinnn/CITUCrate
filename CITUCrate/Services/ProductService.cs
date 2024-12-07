using CITUCrate.DTO;
using CITUCrate.Models;
using CITUCrate.Repositories;

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
                Quantity = p.Quantity
            }).ToList();
        }

        public async Task<bool> AddProductAsync(AddProductDTO addProductDto, IFormFile imageFile)
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
                ImageUrl = imageUrl
            };

            await _productRepository.AddProductAsync(newProduct);
            return true;
        }
    }
}
