﻿using System;
namespace StockShopAPI.Models
{
	public class OrderItem
	{
		public int Id { get; set; }
		public Order Order { get; set; }
		public Product Product { get; set; }
		public int Quantity { get; set; }
		public float Price { get; set; }
	}
}

