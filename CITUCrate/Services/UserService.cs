using CITUCrate.DTO;
using CITUCrate.Models;
using CITUCrate.Services;
using CITUCrate.Repositories;


public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // Updated RegisterUserAsync to accept RegisterDTO
    public async Task<string> RegisterUserAsync(RegisterDTO registerData)
    {
        // Check if user already exists
        var existingUser = await _userRepository.GetUserByEmailAsync(registerData.Email);
        if (existingUser != null)
        {
            return "User already exists.";
        }

        // Map RegisterDTO to User model
        var user = new User
        {
            Username = registerData.Username,
            Email = registerData.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(registerData.Password),
            Balance = 0,
            isBuyer = 1,
        };

        // Add the user to the repository
        await _userRepository.AddUserAsync(user);
        return "Registration successful.";
    }


    // Login functionality
    public async Task<User> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return user;
        }
        return null; // Invalid login
    }


    public async Task<SellerDashboardDTO> GetSellerDashboardAsync(string username)
    {
        // Fetch user from the repository
        var user = await _userRepository.GetUserByUsernameAsync(username);

        // Check if user is null and handle it appropriately
        if (user == null)
        {
            // You can return a default SellerDashboardDTO or throw an exception
            // Depending on how you want to handle this scenario
            return null; // Or you can return a default SellerDashboardDTO
        }

        // Return the SellerDashboardDTO if user is found
        return new SellerDashboardDTO
        {
            Username = user.Username,
            Balance = user.Balance
        };
    }

    public async Task<List<UserDashboardDTO>> GetAllUserDashboardDTOAsync()
    {
        var users = await _userRepository.GetAllUserAsync();

        return users.Select(p => new UserDashboardDTO
        {
            Id = p.Id,
            Username = p.Username,
            Email = p.Email,
            Balance = p.Balance,
            isBuyer = p.isBuyer
        }).ToList();
    }


}
