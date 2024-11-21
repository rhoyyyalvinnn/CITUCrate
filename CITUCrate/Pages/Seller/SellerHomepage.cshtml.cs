using CITUCrate.Models; // Assuming Product model is in this namespace
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using CITUCrate.Migrations;

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

        public async Task OnGetAsync()
        {
            Username = HttpContext.Session.GetString("Username") ?? "Guest";
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == Username);

            decimal balance = user.Balance;

            ViewData["UserBalance"] = balance;

            Products = await _context.Products?.ToListAsync() ?? new List<Product>();

            _logger.LogInformation("Number of products fetched: {ProductCount}", Products.Count);
        }

    }
}
