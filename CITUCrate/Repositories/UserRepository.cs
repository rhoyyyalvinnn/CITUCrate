using CITUCrate.DTO;
using CITUCrate.Models;
using Microsoft.EntityFrameworkCore;

namespace CITUCrate.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> TopUpBalanceAsync(string username, decimal amount)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user != null)
            {
                user.Balance += amount; // Increment balance
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
