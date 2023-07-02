using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StockShopAPI.Models;

namespace StockShopAPI.Data
{
	public class AppDbContext : DbContext
	{
		protected readonly IConfiguration Configuration;

		public AppDbContext(IConfiguration configuration)
		{
			Configuration = configuration;
		}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
			if (!options.IsConfigured)
			{
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

		public DbSet<User> Users { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

