using System;
namespace StockShopAPI.Models.Dto
{
    public class ReviewCreateDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; } = string.Empty;
    }
}

