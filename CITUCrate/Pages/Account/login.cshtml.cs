using CITUCrate.DTO;
using CITUCrate.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CITUCrate.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
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

            // Store the UserId in the session
            _httpContextAccessor.HttpContext.Session.SetInt32("UserId", user.Id);

            _httpContextAccessor.HttpContext.Session.SetString("Username", user.Username);

            // Redirect based on user type
            if (user.isBuyer == 1)
            {
                return RedirectToPage("/Buyer/BuyerHomepage");
            }
            else
            {
                return RedirectToPage("/Seller/SellerHomepage");
            }
        }

    }
}
