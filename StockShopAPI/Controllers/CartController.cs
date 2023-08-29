using System;
using Microsoft.AspNetCore.Mvc;
using StockShopAPI.Models;
using StockShopAPI.Repositories;

namespace StockShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private CartRepository _cartRepository;
        public CartController(CartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateCart(Cart cart)
        {
            await _cartRepository.CreateCart(cart);
            return Ok(new { message = "Cart created" });
        }
    }
}

