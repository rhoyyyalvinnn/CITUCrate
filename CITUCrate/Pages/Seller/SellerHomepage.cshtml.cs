using CITUCrate.Models; // Assuming Product model is in this namespace
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CITUCrate.Pages.Seller
{
    public class SellerHomepageModel : PageModel
    {
        private readonly UserContext _context;

        public SellerHomepageModel(UserContext context)
        {
            _context = context;
        }

        public string Username { get; set; }

        // List of products to display on the homepage
        public List<Product> Products { get; set; }

        // Method that runs when the page is loaded
        public async Task OnGetAsync()
        {
            // Retrieve the Username from the session
            Username = HttpContext.Session.GetString("Username") ?? "Guest";

            // Fetch all products from the database
            Products = await _context.Products.ToListAsync();
        }
    }
}
