using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XuongMay.Models.Entity;

namespace XuongMay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly XuongMayContext _dbContext;

        public AccountController(XuongMayContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Account
        [HttpGet]
        public IActionResult GetAccounts()
        {
            var accounts = _dbContext.Accounts.ToList();
            return Ok(accounts);
        }

        // GET: api/Account/{id}
        [HttpGet("{id}")]
        public IActionResult GetAccount(int id)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.UserId == id);
            if (account == null)
                return NotFound();

            return Ok(account);
        }

        // GET: api/Account/Active
        [HttpGet("Active")]
        public IActionResult GetActiveAccounts()
        {
            var activeAccounts = _dbContext.Accounts.Where(a => a.Status).ToList();
            return Ok(activeAccounts);
        }

        // PUT: api/Account/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, Account updatedAccount)
        {
            var existingAccount = await _dbContext.Accounts.FindAsync(id);
            if (existingAccount == null)
                return NotFound();

            // Update properties as needed
            existingAccount.UserName = updatedAccount.UserName;
            existingAccount.UserPassword = updatedAccount.UserPassword;
            existingAccount.Role = updatedAccount.Role;
            existingAccount.Status = updatedAccount.Status;

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Account/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _dbContext.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();

            // Mark the account as inactive
            account.Status = false; // Set to inactive

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
