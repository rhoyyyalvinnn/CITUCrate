using Microsoft.EntityFrameworkCore;

namespace CITUCrate.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        // Add DbSet for Product model
        public DbSet<Product> Products { get; set; }

        public UserContext(DbContextOptions options) : base(options)
        {
        }
    }
}
