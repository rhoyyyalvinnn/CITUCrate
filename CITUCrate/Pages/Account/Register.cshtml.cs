using CITUCrate.DTO; // Add the DTO namespace
using CITUCrate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace CITUCrate.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserContext _context;

        public RegisterModel(UserContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RegisterDTO RegisterData { get; set; }  // Change to RegisterDTO

        // On GET - Display registration form (unchanged)
        public void OnGet()
        {
        }

        // On POST - Handle registration form submission
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check for reserved usernames
            string[] reservedUsernames = { "Admin", "adminseller" }; // Add other reserved usernames here
            if (reservedUsernames.Contains(RegisterData.Username, StringComparer.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("RegisterData.Username", "This username is reserved and cannot be used.");
                return Page();
            }

            // Check if passwords match
            if (RegisterData.Password != RegisterData.ConfirmPassword)
            {
                ModelState.AddModelError("RegisterData.ConfirmPassword", "Passwords do not match.");
                return Page();
            }

            if (!RegisterData.Email.EndsWith("@cit.edu"))
            {
                ModelState.AddModelError("RegisterData.Email", "Email must end with @cit.edu.");
                return Page();
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(RegisterData.Password);

            var user = new User
            {
                Username = RegisterData.Username,
                Email = RegisterData.Email,
                Password = hashedPassword,
                isBuyer = 1,
                Balance = 0.00m
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Account/Login");
        }
    }
}
