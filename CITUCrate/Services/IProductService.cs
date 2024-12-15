using CITUCrate.DTO;

namespace CITUCrate.Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductDTOsAsync();
        Task<bool> AddProductAsync(AddUpdateProductDTO addProductDto, IFormFile imageFile); // Returns true if successful
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task <bool> UpdateProductAsync(int productId, AddUpdateProductDTO updateProductDTO, IFormFile imageFile);
        Task<List<ProductDTO>> GetProductsByCategoryAsync(string category);
    }
}
