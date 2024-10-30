using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dvdrental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("addcategory")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequestDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Category data is required!");
            }

            var category = await _categoryService.AddCategory(categoryDto);
            return Ok(category);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequestDto categoryDto)
        {
            if (categoryDto == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid category data!");
            }

            await _categoryService.UpdateCategory(id, categoryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }


        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }



    }
}
