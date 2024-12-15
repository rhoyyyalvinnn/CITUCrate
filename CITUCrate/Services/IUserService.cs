using CITUCrate.DTO;
using CITUCrate.Models;

namespace CITUCrate.Services
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(RegisterDTO registerData);
        Task<User> LoginAsync(string email, string password);
        Task<SellerDashboardDTO> GetSellerDashboardAsync(string username);

        Task<List<UserDashboardDTO>> GetAllUserDashboardDTOAsync();
        Task<string> TopUpBalanceAsync(TopUpDTO topUpDTO, string username);
        Task<User> GetUserByUsernameAsync(string username);
    }

}
