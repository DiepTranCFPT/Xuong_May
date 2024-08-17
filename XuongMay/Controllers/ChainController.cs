using Microsoft.AspNetCore.Mvc;
using XuongMay.Models;
using XuongMay.Models.Entity;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace XuongMay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChainController : ControllerBase
    {
        private readonly XuongMayContext dbContext;

        public ChainController(XuongMayContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create a new Chain
        [HttpPost]
        public async Task<IActionResult> CreateChain(ChainDto chainDto)
        {
            var chain = new Chain
            {
                Name = chainDto.Name,
                Description = chainDto.Description,
                CreatedTime = DateTime.UtcNow,
                IsDeleted = false
            };

            dbContext.Chains.Add(chain);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChain), new { id = chain.Id }, chain);
        }

        // Get a Chain by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChain(int id)
        {
            var chain = await dbContext.Chains
                .Include(c => c.Tasks)
                .Include(c => c.Account)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (chain == null)
            {
                return NotFound();
            }

            return Ok(chain);
        }

        // Update a Chain
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChain(int id, Chain updatedChain)
        {
            if (id != updatedChain.Id)
            {
                return BadRequest();
            }

            var existingChain = await dbContext.Chains.FindAsync(id);

            if (existingChain == null)
            {
                return NotFound();
            }

            existingChain.Name = updatedChain.Name;
            existingChain.Description = updatedChain.Description;
            existingChain.ManagerId = updatedChain.ManagerId;
            existingChain.IsDeleted = updatedChain.IsDeleted;

            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        // Delete a Chain
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChain(int id)
        {
            var chain = await dbContext.Chains.FindAsync(id);

            if (chain == null)
            {
                return NotFound();
            }

            dbContext.Chains.Remove(chain);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        // Get all Chains (optional: include related entities)
        [HttpGet]
        public async Task<IActionResult> GetAllChains()
        {
            var chains = await dbContext.Chains
                .Include(c => c.Tasks)
                .Include(c => c.Account)
                .ToListAsync();

            return Ok(chains);
        }
    }


}
