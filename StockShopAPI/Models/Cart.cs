using System;
namespace StockShopAPI.Models
{
	public class Cart
	{
		public Cart()
		{
		}
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Products { get; set; }
	}
}

