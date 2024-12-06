using CITUCrate.DTO;
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
    }
}
