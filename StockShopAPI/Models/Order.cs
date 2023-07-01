using System;
namespace StockShopAPI.Models
{
	public class Order
	{
		public int Id { get; set; }
		public User User { get; set; }
		public DateTime OrderDate { get; set; }
		public float TotalAmount { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; }
	}
}

