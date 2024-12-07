using CITUCrate.DTO;

namespace CITUCrate.Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductDTOsAsync();
        Task<bool> AddProductAsync(AddProductDTO addProductDto, IFormFile imageFile); // Returns true if successful
    }
}
