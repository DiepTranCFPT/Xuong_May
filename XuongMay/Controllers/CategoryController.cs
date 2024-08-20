using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XuongMay.Models;
using XuongMay.Models.Entity;

namespace XuongMay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly XuongMayContext _dbContext;

        public CategoryController(XuongMayContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategory(int pageNumber = 1, int pageSize = 10)
        {
            //var allCategories = await _dbContext.Categories.ToListAsync();
            //return Ok(allCategories);

            var category = await _dbContext.Categories
                                   .Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            if (!category.Any())
            {
                return BadRequest("Don't have any products");
            }

            return Ok(category);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var Categories = await _dbContext.Categories.FindAsync(id);
            if (Categories == null)
            {
                return BadRequest("Category not found");
            }
            return Ok(Categories);
        }
        [HttpPost]
        public async Task<ActionResult<List<Category>>> AddCategory(AddCategoryDto addCategoryDto)
        {
            if (addCategoryDto == null)
            {
                return BadRequest("no data");
            }
            var categoryEntity = new Category()
            {
                Name = addCategoryDto.Name,
                Description = addCategoryDto.Description,
                IsDeleted = false,
            };
            _dbContext.Categories.Add(categoryEntity);
            await _dbContext.SaveChangesAsync();
            return Ok(await GetAllCategory());
        }
        [HttpPut]
        public async Task<ActionResult<List<Category>>> UpdateCate(UpdateCategoryDto updateCate)
        {
            var db = await _dbContext.Categories.FindAsync(updateCate.Id);
            if (db is null) { return BadRequest("category not found"); }
            db.Name = updateCate.Name;
            db.Description = updateCate.Description;
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Categories.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Category>>> DeleteCate(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null || category.IsDeleted)
            {
                return NotFound(new { message = "Category not found or already deleted." });
            }
            category.IsDeleted = true;
            //_dbContext.Categories.Remove(db);
            await _dbContext.SaveChangesAsync();
            return Ok("Category deleted successfully.");
        }
    }

}
