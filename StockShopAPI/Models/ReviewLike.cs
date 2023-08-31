using System;
namespace StockShopAPI.Models
{
    public class ReviewLike
    {
        public int UserId { get; set; }
        public int ReviewId { get; set; }
        public DateTime LikeDate { get; set; }
    }
}

