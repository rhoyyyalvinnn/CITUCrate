using CITUCrate.DTO;
using CITUCrate.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CITUCrate.Pages.Seller
{
    public class SellerHomepageModel : PageModel
    {
        private readonly UserContext _context;
        private readonly ILogger<SellerHomepageModel> _logger;

        public SellerHomepageModel(UserContext context, ILogger<SellerHomepageModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public SellerDashboardDTO SellerDashboard { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            // Get the username from the session
            var username = HttpContext.Session.GetString("Username") ?? "Guest";
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                // Handle case where user is not found
                // Redirect to login or show an error
                return;
            }

            // Create the DTO to pass to the view
            SellerDashboard = new SellerDashboardDTO
            {
                Username = user.Username,
                Balance = user.Balance,
                Products = await GetProductDTOs(sortOrder)
            };
        }

        private async Task<List<ProductDTO>> GetProductDTOs(string sortOrder)
        {
            var productsQuery = _context.Products.AsQueryable();

            // Apply sorting logic
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "category_desc" : "Category";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["QuantitySortParm"] = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";

            switch (sortOrder)
            {
                case "name_desc":
                    productsQuery = productsQuery.OrderByDescending(p => p.Name);
                    break;
                case "Category":
                    productsQuery = productsQuery.OrderBy(p => p.Category);
                    break;
                case "category_desc":
                    productsQuery = productsQuery.OrderByDescending(p => p.Category);
                    break;
                case "Price":
                    productsQuery = productsQuery.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    productsQuery = productsQuery.OrderByDescending(p => p.Price);
                    break;
                case "Quantity":
                    productsQuery = productsQuery.OrderBy(p => p.Quantity);
                    break;
                case "quantity_desc":
                    productsQuery = productsQuery.OrderByDescending(p => p.Quantity);
                    break;
                default:
                    productsQuery = productsQuery.OrderBy(p => p.Name);
                    break;
            }

            var products = await productsQuery.ToListAsync();

            return products.Select(p => new ProductDTO
            {
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToList();
        }
    }
}

