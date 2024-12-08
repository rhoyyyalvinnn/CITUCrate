using CITUCrate.Models;
using CITUCrate.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CITUCrate.DTO;

namespace CITUCrate.Pages.Seller
{
    public class AccountManagementModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountManagementModel> _logger;

        // Constructor
        public AccountManagementModel(IUserService userService, ILogger<AccountManagementModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // Property to store user data
        public List<UserDashboardDTO> Users { get; set; }

        // Fetch users from the service and populate the Users property
        public async Task OnGetAsync()
        {
            try
            {
                Users = await _userService.GetAllUserDashboardDTOAsync();

                if (Users == null || !Users.Any())
                {
                    _logger.LogWarning("No users found in the database.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users.");
                Users = new List<UserDashboardDTO>(); // Ensure Users is initialized to avoid null reference issues
            }
        }
    }
}
