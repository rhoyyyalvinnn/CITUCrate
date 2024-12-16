using CITUCrate.DTO;
using CITUCrate.Models;
using CITUCrate.Repositories;
using CITUCrate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CITUCrate.Pages.Seller
{
    public class SellerHomepageModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly ILogger<SellerHomepageModel> _logger;
        private readonly IProductRepository _productRepository;

        public SellerHomepageModel(IUserService userService, IProductService productService, ILogger<SellerHomepageModel> logger, IProductRepository productRepository)
        {
            _userService = userService;
            _productService = productService;
            _logger = logger;
        }
        public SellerDashboardDTO SellerDashboard { get; set; }
        public List<ProductDTO> Products { get; set; }
        public async Task OnGetAsync(string sortOrder)
        {
            // Get the username from the session
            var username = HttpContext.Session.GetString("Username") ?? "Guest";
            var user = await _userService.GetSellerDashboardAsync(username);



            if (user == null)
            {
                // Handle case where user is not found (redirect or show error)
                _logger.LogWarning("User not found for username: {Username}", username);
                RedirectToPage("/Account/Login");
                return;
            }

            // Populate SellerDashboardDTO
            SellerDashboard = new SellerDashboardDTO
            {
                Username = user.Username,
                Balance = user.Balance
            };

            // Fetch and sort products
            var products = await _productService.GetAllProductDTOsAsync();
            Products = ApplySorting(products, sortOrder);
        }
        private List<ProductDTO> ApplySorting(List<ProductDTO> products, string sortOrder)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "category_desc" : "Category";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["QuantitySortParm"] = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewData["TotalSalesSortParm"] = sortOrder == "TotalSales" ? "totalsales_desc" : "TotalSales";

            return sortOrder switch
            {
                "name_desc" => products.OrderByDescending(p => p.Name).ToList(),
                "Category" => products.OrderBy(p => p.Category).ToList(),
                "category_desc" => products.OrderByDescending(p => p.Category).ToList(),
                "Price" => products.OrderBy(p => p.Price).ToList(),
                "price_desc" => products.OrderByDescending(p => p.Price).ToList(),
                "Quantity" => products.OrderBy(p => p.Quantity).ToList(),
                "quantity_desc" => products.OrderByDescending(p => p.Quantity).ToList(),
                "TotalSales" => products.OrderBy(p => p.TotalSales).ToList(),
                "totalsales_desc" => products.OrderByDescending(p => p.TotalSales).ToList(),
                _ => products.OrderBy(p => p.Name).ToList(),
            };
        }

        public async Task<IActionResult> OnPostUpdateAsync(int productID, IFormCollection form, IFormFile imageFile)
        {
            if (productID == 0)
            {
                _logger.LogWarning("Product ID not found or invalid.");
                return NotFound(); // If product ID is invalid, return not found
            }

            // Log the product ID being updated
            _logger.LogInformation("Updating product with ID: {productID}. Form data: Name={Name}, Category={Category}, Price={Price}, Quantity={Quantity}",
                productID, // Log the product ID being updated
                form["Name"],
                form["Category"],
                form["Price"],
                form["Quantity"]);

            var updateProductDTO = new AddUpdateProductDTO
            {
                Name = form["Name"],
                Category = form["Category"],
                Price = decimal.TryParse(form["Price"], out var price) ? price : 0m,
                Quantity = int.TryParse(form["Quantity"], out var quantity) ? quantity : 0,
                ShortDescription = form["ShortDescription"]
            };

            var success = await _productService.UpdateProductAsync(productID, updateProductDTO, imageFile);

            if (!success)
            {
                _logger.LogWarning("Product with ID {ProductId} could not be updated.", productID);
                return NotFound();
            }

            return RedirectToPage();
        }
    }
}