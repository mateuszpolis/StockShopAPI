using System;
namespace StockShopAPI.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Producer { get; set; }
		public float Price { get; set; }
		public float PriceBefore { get; set; }
		public string Categories { get; set; }
		public string Img { get; set; }
		public float Rating { get; set; }
	}
}

