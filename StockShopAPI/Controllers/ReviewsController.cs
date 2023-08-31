using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockShopAPI.Models;
using StockShopAPI.Models.Dto;
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
        [Authorize]
        public async Task<IActionResult> CreateReview(ReviewCreateDTO review)
        {
            bool reviewCreated = await _reviewRepository.Create(review);
            if (reviewCreated)
            {
                return Ok(new { message = "Review created succesfuly" });
            }
            else
            {
                return BadRequest(new { message = "Product does not exist or could not be found " });
            }

        }

        [HttpPut("Like")]
        [Authorize]
        public async Task<IActionResult> LikeReview([FromBody] LikeReviewRequestDTO request)
        {
            Console.WriteLine(request.UserId);
            Console.WriteLine(request.ReviewId);

            bool reviewLiked = await _reviewRepository.LikeReview(request.UserId, request.ReviewId);
            if (reviewLiked)
            {
                return Ok(new { message = "Review liked" });
            }
            else
            {
                return BadRequest(new { message = "Review not liked" });
            }
        }
    }
}

