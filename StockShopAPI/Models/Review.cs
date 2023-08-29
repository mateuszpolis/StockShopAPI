using System;
namespace StockShopAPI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; } = string.Empty;
        public int Likes { get; set; }
        public bool OwnsProduct { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}

