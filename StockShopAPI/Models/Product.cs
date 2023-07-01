using System;
namespace StockShopAPI.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public float PriceLow { get; set; }
		public float PriceHigh { get; set; }
		public float PriceCurrent { get; set; }
		public int Quantity { get; set; }
		public ICollection<Category> Categories { get; set; }
        public ICollection<Review> Reviews { get; set; }
	}
}

