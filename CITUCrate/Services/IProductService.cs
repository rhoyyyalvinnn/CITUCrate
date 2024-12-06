using CITUCrate.DTO;

namespace CITUCrate.Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductDTOsAsync();
    }
}
