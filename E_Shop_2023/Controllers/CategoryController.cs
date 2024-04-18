using Infrastructure.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace E_Shop_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categorySrv;
        public CategoryController(ICategoryService categoryService)
        {
            _categorySrv = categoryService;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = new List<CategoryDTO>();
            var categories = await _categorySrv.GetAllCategories();

            foreach (var category in categories)
            {
                result.Add(new CategoryDTO
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                });
            }
            return Ok(result);
        }
    }
}
