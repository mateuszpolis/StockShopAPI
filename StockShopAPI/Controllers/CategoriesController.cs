using System;
using Microsoft.AspNetCore.Mvc;
using StockShopAPI.Models;
using StockShopAPI.Repositories;

namespace StockShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private CategoryRepository _categoryRepository;
        public CategoriesController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategories(int id)
        {
            var categories = await _categoryRepository.GetCategories(id);
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            await _categoryRepository.Create(category);
            return Ok("Category created succesfuly");
        }
    }
}

