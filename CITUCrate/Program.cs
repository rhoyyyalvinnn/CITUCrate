using CITUCrate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace CITUCrate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\dbCITUCrate;Database=UserDB;Trusted_Connection=True;TrustServerCertificate=True;");
            });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout as needed
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            

            var app = builder.Build();

            // Seed admin user if not exists
            SeedAdminUser(app);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }

        private static void SeedAdminUser(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<UserContext>();

                // Check if the admin user already exists
                if (!context.Users.Any(u => u.Username == "AdminSeller"))
                {
                    // Create the admin user
                    var adminUser = new User
                    {
                        Username = "AdminSeller",
                        Email = "adminseller@admin.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("adminseller123"), // Hash the password
                        isBuyer = 0 // Set as admin (assuming 0 means admin)
                    };

                    context.Users.Add(adminUser);
                    context.SaveChanges(); // Save changes to the database
                }
            }
        }
    }
}
