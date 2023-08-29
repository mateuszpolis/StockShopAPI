using System;
using Microsoft.AspNetCore.Mvc;
using StockShopAPI.Models;
using StockShopAPI.Repositories;

namespace StockShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductRepository _productRepository;

        public ProductsController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> CreateProduct(Product request)
        {
            await _productRepository.Create(request);

            return Ok(new { message = "Product created succesfuly" });
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(string searchQuery, int category, string sorting, int limit)
        {
            var products = await _productRepository.GetProducts(searchQuery, category, sorting, limit);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetById(id);
            return Ok(product);
        }

    }
}

