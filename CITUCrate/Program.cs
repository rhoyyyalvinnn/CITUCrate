using CITUCrate.Models;
using CITUCrate.Repositories;
using CITUCrate.Services;
using Microsoft.EntityFrameworkCore;
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
                var connectionString = builder.Configuration.GetConnectionString("UserDB");
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddScoped<IProductRepository, ProductRepository>();  // Scoped because ProductRepository typically needs a scoped lifetime
            builder.Services.AddTransient<IProductService, ProductService>();  // Transient is okay for services

            builder.Services.AddScoped<IUserRepository, UserRepository>(); // Scoped for UserRepository
            builder.Services.AddScoped<IUserService, UserService>(); // Use Scoped for services interacting with 
            builder.Services.AddScoped<IOrdersRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout as needed
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            builder.Services.AddLogging();

            var app = builder.Build();

            // Seed admin user if not exists
            DatabaseSeeder.SeedAdminUser(app.Services);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
        }
    }
}
