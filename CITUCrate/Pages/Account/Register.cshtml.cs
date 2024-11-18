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
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public int isBuyer { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check for reserved usernames
            string[] reservedUsernames = { "Admin", "adminseller" }; // Add other reserved usernames here
            if (reservedUsernames.Contains(Input.Username, StringComparer.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Input.Username", "This username is reserved and cannot be used.");
                return Page();
            }

            // Hash the password before saving
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Input.Password);

            // Create a new user with balance set to 0.00
            var user = new User
            {
                Username = Input.Username,
                Email = Input.Email,
                Password = hashedPassword,
                isBuyer = 1, // Default value, adjust as needed
                Balance = 0.00m  // Set the balance to 0.00
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Redirect to the login page after successful registration
            return RedirectToPage("/Account/Login");
        }
    }
}
