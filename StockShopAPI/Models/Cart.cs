using System;
namespace StockShopAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
    }
}

