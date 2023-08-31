using System;
namespace StockShopAPI.Models.Dto
{
    public class LikeReviewRequestDTO
    {
        public int UserId { get; set; }
        public int ReviewId { get; set; }
    }
}

