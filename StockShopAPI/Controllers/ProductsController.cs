using System;
using Microsoft.AspNetCore.Mvc;
using StockShopAPI.Data;
using StockShopAPI.Models;

namespace StockShopAPI.Controllers
{
	[Route("api/Products")]
	[ApiController]
	public class ProductsController: ControllerBase
    {
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<IEnumerable<Product>> GetProducts()
		{
            Console.WriteLine("ce");
			var productList = ProductStore.productList;
			if (productList == null)
			{
				return NotFound();
			}
			return Ok(productList);
		}
	}
}

