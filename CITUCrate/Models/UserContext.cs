using Microsoft.EntityFrameworkCore;


namespace CITUCrate.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();  // Enable lazy loading
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for Subtotal in OrderItem
            modelBuilder.Entity<OrderItem>()
                .Property(o => o.Subtotal)
                .HasPrecision(18, 2); // 18 digits total, 2 digits after the decimal point

            // Configure decimal precision for Balance in User
            modelBuilder.Entity<User>()
                .Property(u => u.Balance)
                .HasPrecision(18, 2); // 18 digits total, 2 digits after the decimal point

            modelBuilder.Entity<Order>()
                .Property(o => o.Total)
                .HasColumnType("decimal(18,2)"); // 18 total digits, 2 decimal places

        }
    }
}
