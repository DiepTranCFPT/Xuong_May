using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using XuongMay.Models;
using XuongMay.Models.Entity;

namespace XuongMay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly XuongMayContext _dbContext;
        public ProductController(XuongMayContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProduct()
        {
            var products = await _dbContext.Products.ToListAsync();
            if (products is null)
            {
                return BadRequest("Don't have any product");
            }
            return Ok(products);
        }
        [HttpGet("search")]
        public async Task<ActionResult<List<Product>>> SearchProducts(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Product name cannot be empty.");
            }

            var products = await _dbContext.Products.Where(p => p.Name.Contains(name)).ToListAsync();

            if (!products.Any())
            {
                return NotFound("No products found.");
            }

            return Ok(products);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto updateProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if (updateProductDto == null)
            //{
            //    return BadRequest("Invalid product data.");
            //}
            if (!await _dbContext.Categories.AnyAsync(c => c.Id == updateProductDto.CategoryID))
            {
                return BadRequest("CategoryID does not exist.");
            }
            var product = await _dbContext.Products.FindAsync(updateProductDto.ProductID);

            if (product == null)
            {
                return BadRequest("don't find out product");
            }

            product.CategoryID = updateProductDto.CategoryID;
            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Amount = updateProductDto.Amount;
            product.UnitPrice = updateProductDto.UnitPrice;
            product.Status = updateProductDto.Status;

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            return Ok("Product updated successfully.");
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto createProductDto)
        {
            if (createProductDto == null)
            {
                return BadRequest("Invalid product data.");
            }
            if (!await _dbContext.Categories.AnyAsync(c => c.Id == createProductDto.CategoryID))
            {
                throw new ValidationException("CategoryID does not exist.");
            }
            var product = new Product
            {
                CategoryID = createProductDto.CategoryID,
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Amount = createProductDto.Amount,
                UnitPrice = createProductDto.UnitPrice,
                Status = createProductDto.Status
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(AddProduct), new { id = product.ProductID }, product);
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            //if (!int.TryParse(id, out int productId) || productId <= 0)
            //{
            //    return BadRequest("Invalid ID. ID must be a positive number.");
            //}
            if (id <= 0)
            {
                return BadRequest("Invalid ID. ID must be a positive number.");
            }
            var db = await _dbContext.Products.FindAsync(id);
            if (db is null) { return BadRequest("Product not found"); }
            _dbContext.Products.Remove(db);
            await _dbContext.SaveChangesAsync();
            return Ok("Product deleted successfully");
        }
    }
}
