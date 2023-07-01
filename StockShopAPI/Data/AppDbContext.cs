using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        }

		public DbSet<Models.User> Users { get; set; }
		public DbSet<Models.Review> Reviews { get; set; }
		public DbSet<Models.Product> Products { get; set; }
		public DbSet<Models.OrderItem> OrderItems { get; set; }
		public DbSet<Models.Order> Orders { get; set; }
		public DbSet<Models.Category> Categories { get; set; }
		public DbSet<Models.CartItem> CartItems { get; set; }
		public DbSet<Models.Cart> Carts { get; set; }
    }
}

