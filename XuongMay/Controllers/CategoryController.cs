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
        public async Task<ActionResult<List<Category>>> GetAllCategory()
        {
            var allCategories = await _dbContext.Categories.ToListAsync();
            return Ok(allCategories);
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
            var db = await _dbContext.Categories.FindAsync(id);
            if (db is null) { return BadRequest("category not found"); }
            _dbContext.Categories.Remove(db);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Categories.ToListAsync());
        }
    }

}
