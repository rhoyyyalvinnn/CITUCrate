﻿using CITUCrate.DTO;
using CITUCrate.Models;

namespace CITUCrate.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task<User> GetUserByUsernameAsync(string username);
        Task<List<User>> GetAllUserAsync();
        Task<bool> TopUpBalanceAsync(string username, decimal amount);
    }

}
