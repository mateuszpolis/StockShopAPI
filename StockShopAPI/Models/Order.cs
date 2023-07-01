using System;
namespace StockShopAPI.Models
{
	public class Order
	{
		public Order()
		{
		}
		public int Id { get; set; }
		public int UserId { get; set; }
		public DateTime OrderDate { get; set; }
		public float TotalAmount { get; set; }
	}
}

