using CITUCrate.DTO;
using CITUCrate.Services;
using CITUCrate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CITUCrate.Pages.Seller;

namespace CITUCrate.Pages.Buyer
{
    public class BuyerCategoriesModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ILogger<BuyerCategoriesModel> _logger;

        // Constructor
        public BuyerCategoriesModel(IProductService productService, ILogger<BuyerCategoriesModel> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // Property to store product data
        public List<ProductDTO> Products { get; set; }

        public string Category { get; set; }
        // Fetch products from the service and populate the Products property
        public async Task OnGetAsync(string category)
        {
            try
            {
                Category = category;
                Products = await _productService.GetProductsByCategoryAsync(category);

                if (Products == null || !Products.Any())
                {
                    _logger.LogWarning($"No products found for category: {category}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products by category.");
                Products = new List<ProductDTO>();
            }
        }
    }
}
