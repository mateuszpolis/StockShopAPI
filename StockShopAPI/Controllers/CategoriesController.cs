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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var categories = await _categoryRepository.GetCategories(id);
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpGet("{id:int}/hierarchy")]
        public async Task<IActionResult> GetCategoryHierarchy(int id)
        {
            var hierarchy = await _categoryRepository.GetCategoryHierarchy(id);
            return Ok(hierarchy.Reverse());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            await _categoryRepository.Create(category);
            return Ok("Category created successfully");
        }
    }
}
