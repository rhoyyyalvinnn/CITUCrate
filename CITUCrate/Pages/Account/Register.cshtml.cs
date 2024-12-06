using CITUCrate.DTO;
using CITUCrate.Models;
using CITUCrate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CITUCrate.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public RegisterDTO UserData { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _userService.RegisterUserAsync(UserData);

            if (result != "Registration successful.")
            {
                ErrorMessage = result;
                return Page();
            }

            return RedirectToPage("/Account/Login");
        }

    }


}
