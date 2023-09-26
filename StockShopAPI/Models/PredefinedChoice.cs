using System;
namespace StockShopAPI.Models
{
    public class PredefinedChoice
    {
        public int Id { get; set; }
        public int ParameterId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
    }
}

