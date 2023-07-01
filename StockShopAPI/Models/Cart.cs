using System;
namespace StockShopAPI.Models
{
	public class Cart
	{
		public int Id { get; set; }
		public User User { get; set; }
		public float TotalAmount { get; set; }
		public ICollection<CartItem> CartItems { get; set; }
	}
}

