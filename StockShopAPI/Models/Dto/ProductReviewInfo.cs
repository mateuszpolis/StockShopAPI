using System;
namespace StockShopAPI.Models.Dto
{
    public class ProductReviewInfo
    {
        public int Id { get; set; }
        public float Rating { get; set; }
        public int NumberOfReviews { get; set; }
    }
}

