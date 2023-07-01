using System;
namespace StockShopAPI.Models
{
	public class Review
	{
		public Review()
		{
		}
		public int Id { get; set; }
		public int UserId { get; set; }
		public int ProductId { get; set; }
		public int Rating { get; set; }
		public string ReviewText { get; set; }
		public DateTime ReviewDate { get; set; } 
	}
}

