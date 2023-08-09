using System;
namespace StockShopAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool HasChildren { get; set; } = false;
        public int? ParentCategory { get; set; }
        public int Transactions { get; set; }
        public int Visits { get; set; }
    }
}

