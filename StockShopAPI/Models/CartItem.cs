using System;
namespace StockShopAPI.Models
{
	public class CartItem
	{
		public int Id { get; set; }
		public Cart Cart { get; set; }
		public Product Product { get; set; }
		public int Quantity { get; set; }
		public float Price { get; set; }
	}
}	
