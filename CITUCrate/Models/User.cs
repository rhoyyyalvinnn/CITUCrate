﻿namespace CITUCrate.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; } = 0.00m;
        public int isBuyer { get; set; } = 0;

    }

}
