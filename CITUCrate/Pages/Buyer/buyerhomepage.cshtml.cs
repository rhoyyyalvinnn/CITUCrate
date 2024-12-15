using CITUCrate.DTO;
using CITUCrate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CITUCrate.Pages.Buyer
{
    public class buyerhomepageModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ILogger<buyerhomepageModel> _logger;

        // Constructor
        public buyerhomepageModel(IProductService productService, ILogger<buyerhomepageModel> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // Property to store product data
        public List<ProductDTO> Products { get; set; }
        public List<ProductDTO> PopularProducts { get; set; }
        public async Task OnGetAsync()
        {
            try
            {
                Products = await _productService.GetAllProductDTOsAsync();

                if (Products == null || !Products.Any())
                {
                    _logger.LogWarning($"No products found");
                }
                else
                {
                    // Get the top 5 popular products by TotalSales
                    PopularProducts = Products
                        .OrderByDescending(p => p.TotalSales)
                        .Take(5)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products");
                Products = new List<ProductDTO>();
                PopularProducts = new List<ProductDTO>();
            }
        }
    }
}
