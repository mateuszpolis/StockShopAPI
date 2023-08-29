using System;
using Microsoft.AspNetCore.Mvc;
using StockShopAPI.Models;
using StockShopAPI.Repositories;

namespace StockShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private ReviewRepository _reviewRepository;
        public ReviewsController(ReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetReviews(int productId)
        {
            var reviews = await _reviewRepository.GetReviews(productId);
            if (reviews == null)
            {
                return NotFound();
            }
            return Ok(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(Review review)
        {
            await _reviewRepository.Create(review);
            return Ok("Review created succesfuly");
        }
    }
}

