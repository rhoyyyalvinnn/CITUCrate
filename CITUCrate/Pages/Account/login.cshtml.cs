using CITUCrate.DTO;
using CITUCrate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CITUCrate.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public LoginDTO LoginData { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userService.LoginAsync(LoginData.Email, LoginData.Password);

            if (user == null)
            {
                ErrorMessage = "Invalid email or password.";
                return Page();
            }
            if (user.isBuyer == 1)
            {
                return RedirectToPage("/aboutus");
            }
            else 
            {
                return RedirectToPage("/Seller/SellerHomepage");
            }

        }
    }
}
