using System;
using StockShopAPI.Models;

namespace StockShopAPI.Data
{
	public class ProductStore
	{
		public static List<Product> productList = new List<Product>()
		{
			new Product()
			{
				Id = 0,
				Name = "iPhone 14",
				Producer = "Apple",
				Price = 799,
				PriceBefore = 999,
				Categories = "Smartphones Apple",
				Rating = 4.1f,
				Img = "https://cdn.pixabay.com/photo/2022/09/25/22/25/iphone-7479306_1280.jpg"
            },
			new Product()
			{
                Id = 1,
                Name = "Printer",
                Producer = "HP",
                Price = 99,
                PriceBefore = 129,
                Categories = "Printers HP",
                Rating = 3.6f,
                Img = "https://cdn.pixabay.com/photo/2015/05/30/15/45/printer-790396_1280.jpg"
            },
            new Product()
            {
                Id = 2,
                Name = "Macbook Pro",
                Producer = "Apple",
                Price = 1299,
                Categories = "Laptops Apple",
                Rating = 5f,
                Img = "https://cdn.pixabay.com/photo/2017/05/24/21/33/workplace-2341642_1280.jpg"
            },
            new Product()
            {
                Id = 3,
                Name = "iMac",
                Producer = "Apple",
                Price = 1599,
                Categories = "Laptops Apple",
                Rating = 2f,
                Img = "https://cdn.pixabay.com/photo/2017/05/24/21/33/workplace-2341642_1280.jpg"
            }
        };
	}
}

