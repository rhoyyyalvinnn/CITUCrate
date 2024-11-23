using CITUCrate.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CITUCrate.Pages.Seller
{
    public class AccountManagementModel : PageModel
    {
        private readonly UserContext _context;

        // Property to hold the list of users
        public List<User> Users { get; set; }

        // Inject the UserContext using dependency injection
        public AccountManagementModel(UserContext context)
        {
            _context = context;
        }

        // Fetch users from the database
        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
        }
    }
}
