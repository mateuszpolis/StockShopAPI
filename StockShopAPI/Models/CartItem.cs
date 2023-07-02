using System;
namespace StockShopAPI.Models
{
	public class CartItem
	{
		public int Id { get; set; }
		public int CartId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public float Price { get; set; }
	}
}	
