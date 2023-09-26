using System;
namespace StockShopAPI.Models
{
    public class Parameter
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ParameterType { get; set; } = string.Empty;
        public string PredefinedChoices { get; set; } = string.Empty;
    }
}

