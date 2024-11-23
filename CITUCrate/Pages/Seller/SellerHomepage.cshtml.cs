using CITUCrate.Models; // Assuming Product model is in this namespace
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using CITUCrate.Migrations;
using Microsoft.Data.SqlClient;

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

        public string Username { get; set; }
        public List<Product> Products { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            Username = HttpContext.Session.GetString("Username") ?? "Guest";
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == Username);

            decimal balance = user.Balance;
            ViewData["UserBalance"] = balance;

            // Define sorting parameters
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "category_desc" : "Category";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["QuantitySortParm"] = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";

            var products = from p in _context.Products
                           select p;

            // Apply sorting based on the sortOrder parameter
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "Category":
                    products = products.OrderBy(p => p.Category);
                    break;
                case "category_desc":
                    products = products.OrderByDescending(p => p.Category);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "Quantity":
                    products = products.OrderBy(p => p.Quantity);
                    break;
                case "quantity_desc":
                    products = products.OrderByDescending(p => p.Quantity);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            Products = await products.ToListAsync();

            _logger.LogInformation("Number of products fetched: {ProductCount}", Products.Count);
        }


    }
}
