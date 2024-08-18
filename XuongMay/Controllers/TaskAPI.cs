using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XuongMay.Models.Entity;
using Task = XuongMay.Models.Entity.Task;

namespace XuongMay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskAPI : ControllerBase
    {
        private readonly XuongMayContext dbContext;

        public TaskAPI(XuongMayContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/TaskAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            return await dbContext.Tasks.ToListAsync();
        }

        // GET: api/TaskAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await dbContext.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // POST: api/TaskAPI
        [HttpPost]
        public async Task<ActionResult<Task>> PostTask(Task task)
        {
            dbContext.Tasks.Add(task);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, task);
        }

        // PUT: api/TaskAPI/
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Task task)
        {
            if (id != task.TaskId)
            {
                return BadRequest();
            }

            dbContext.Entry(task).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/TaskAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await dbContext.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            // Set the Status to indicate deletion (e.g., 0 for deleted)
            task.Status = 0;

            dbContext.Entry(task).State = EntityState.Modified;

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return dbContext.Tasks.Any(e => e.TaskId == id);
        }
    }
}
