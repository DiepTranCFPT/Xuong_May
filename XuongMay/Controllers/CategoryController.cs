using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongMay.Models;
using XuongMay.Models.Entity;

namespace XuongMay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly XuongMayContext dbContext;

        public CategoryController(XuongMayContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var allCategories = dbContext.Categories.ToList();
            return Ok(allCategories);
        }
        [HttpPost]
        public IActionResult AddCategory(AddCategoryDto addCategoryDto) {
            var categoryEntity = new Category()
            {
                Name = addCategoryDto.Name,
                Description = addCategoryDto.Description,
                status = true
            };
            return Ok();
        }
    }
}
