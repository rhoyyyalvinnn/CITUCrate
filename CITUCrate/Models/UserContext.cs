using Microsoft.EntityFrameworkCore;

namespace CITUCrate.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions options) : base(options)
        {
        }
    }
}
