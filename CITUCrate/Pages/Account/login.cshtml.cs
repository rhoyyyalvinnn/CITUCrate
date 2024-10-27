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
        public InputModel Input { get; set; }

        public string ErrorMessage { get; set; } // To hold the error message

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
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

            // Check if the user exists in the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Input.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(Input.Password, user.Password))
            {

                if(user.isBuyer == 0) //if admin
                {
                    return RedirectToPage("/aboutus");
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
    }
}
