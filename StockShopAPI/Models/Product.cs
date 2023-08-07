using System;
namespace StockShopAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public float Price { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public List<Tuple<string, string>> Specification = new List<Tuple<string, string>>();
        public List<Tuple<float, DateTime>> PriceHistory { get; set; } = new List<Tuple<float, DateTime>>();
        public int Discount { get; set; } = 0;
        public int Orders { get; set; } = 0;
        public int StockQuantity { get; set; } = 0;
        public int CategoryId { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public bool Availability { get; set; } = false;
        public DateTime CreatedTime { get; set; } = new DateTime();
        public DateTime UpdatedTime { get; set; } = new DateTime();
        public float Weight { get; set; } = 0;
        public string Dimensions { get; set; } = string.Empty;
        public float Rating { get; set; } = 0;
        //public List<Product> RelatedProducts { get; set; } = new List<Product>();
    }
}

