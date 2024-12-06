using BCrypt.Net;
using CITUCrate.Models;

public static class DatabaseSeeder
{
    public static void SeedAdminUser(IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<UserContext>();

            // Check if the admin user already exists
            if (!context.Users.Any(u => u.Username == "AdminSeller"))
            {
                // Temporarily Hardcoded admin credentials but will be replaced later with a more secure implementation
                var adminUsername = "AdminSeller";
                var adminEmail = "adminseller@admin.com";
                var adminPassword = "adminseller123";

                var adminUser = new User
                {
                    Username = adminUsername,
                    Email = adminEmail,
                    Password = BCrypt.Net.BCrypt.HashPassword(adminPassword), 
                    isBuyer = 0 
                };

                context.Users.Add(adminUser);
                context.SaveChanges();
            }
        }
    }
}
