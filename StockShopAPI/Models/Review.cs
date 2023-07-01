using System;
namespace StockShopAPI.Models
{
	public class Review
	{
		public int Id { get; set; }
		public User User { get; set; }
		public Product Product { get; set; }
		public int Rating { get; set; }
		public string ReviewText { get; set; }
		public DateTime ReviewDate { get; set; } 
	}
}

