using CITUCrate.DTO;
using CITUCrate.Models;

namespace CITUCrate.Services
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(RegisterDTO registerData);
        Task<User> LoginAsync(string email, string password);
        Task<SellerDashboardDTO> GetSellerDashboardAsync(string username);

        //Task<User> GetUserAsync(string username);
    }

}
