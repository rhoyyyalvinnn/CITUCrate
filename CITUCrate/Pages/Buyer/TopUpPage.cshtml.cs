using CITUCrate.DTO;
using CITUCrate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace CITUCrate.Pages.Buyer
{
    public class TopUpPageModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ILogger<TopUpPageModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TopUpPageModel(IUserService userService, ILogger<TopUpPageModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public TopUpDTO TopUp { get; set; }

        public decimal CurrentBalance { get; set; }

        public async Task OnGetAsync()
        {
            var username = _httpContextAccessor.HttpContext.Session.GetString("Username");

            if (!string.IsNullOrEmpty(username))
            {
                try
                {
                    var user = await _userService.GetUserByUsernameAsync(username);
                    if (user != null)
                    {
                        CurrentBalance = user.Balance;
                    }
                    else
                    {
                        _logger.LogWarning($"User with username {username} not found.");
                        CurrentBalance = 0;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error retrieving user balance.");
                    CurrentBalance = 0;
                }
            }
            else
            {
                CurrentBalance = 0; // Handle unauthenticated state
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var username = _httpContextAccessor.HttpContext.Session.GetString("Username");

                if (string.IsNullOrEmpty(username))
                {
                    TempData["Message"] = "User is not authenticated.";
                    return RedirectToPage("/Account/Login");
                }

                try
                {
                    var result = await _userService.TopUpBalanceAsync(TopUp, username);

                    if (result == "Top-up successful.")
                    {
                        TempData["Message"] = "Top-up successful!";
                        return RedirectToPage("/Buyer/TopUpPage");
                    }
                    else
                    {
                        TempData["Message"] = "Top-up failed! Please try again.";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during top-up process.");
                    TempData["Message"] = "An error occurred while processing your top-up.";
                }
            }
            else
            {
                TempData["Message"] = "Invalid input. Please check the entered data.";
            }

            return Page();
        }
    }
}
