﻿using CITUCrate.DTO;
using CITUCrate.Models;

namespace CITUCrate.Services
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(RegisterDTO registerData);

        Task<User> LoginAsync(string email, string password);
    }

}
