using CITUCrate.DTO;  // Add the DTO namespace
using CITUCrate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace CITUCrate.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserContext _context;

        public LoginModel(UserContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LoginDTO LoginData { get; set; }  // Use LoginDTO

        public string ErrorMessage { get; set; } // To hold the error message

        // On GET - Display login page (unchanged)
        public void OnGet()
        {
        }

        // On POST - Handle login form submission
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if the user exists in the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == LoginData.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(LoginData.Password, user.Password))
            {
                // Store user info in session
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("UserId", user.Id);

                if (user.isBuyer == 0) // if admin
                {
                    return RedirectToPage("/Seller/SellerHomepage");
                }
                else
                {
                    return RedirectToPage("/index");
                }
            }
            else
            {
                // Set error message for invalid credentials
                ErrorMessage = "Invalid login attempt. The email or password is incorrect.";
                return Page();
            }
        }

        // Optional POST logout method (unchanged)
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Account/Login");
        }
    }
}
