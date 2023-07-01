﻿using System;
namespace StockShopAPI.Models
{
	public class Product
	{
		public Product()
		{
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public float PriceLow { get; set; }
		public float PriceHigh { get; set; }
		public float PriceCurrent { get; set; }
		public int Quantity { get; set; }
		public string Categories { get; set; }
	}
}
