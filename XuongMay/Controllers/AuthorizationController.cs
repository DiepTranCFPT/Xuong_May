using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using XuongMay.Models;
using XuongMay.Models.Entity;

namespace XuongMay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly XuongMayContext _dbContext;

        public AuthorizationController(XuongMayContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("Login")]
        //[AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            // Retrieve user from the database based on UserName
            var user = await _dbContext.Accounts.SingleOrDefaultAsync(x => x.UserName == model.UserName);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // Check if the account is active
            if (!user.Status)
            {
                return BadRequest("Account is inactive. Please contact support.");
            }

            // Validate password using secure hashing
            //if (!VerifyPassword(user.UserPassword, model.UserPassword))
            if (user.UserPassword != model.UserPassword)
            {
                return BadRequest("Invalid password.");
            }

            // Generate a JWT token (you can use your existing method here)
            var token = GenerateJwtToken(user.UserName);

            return Ok(new { Token = token });
        }

        private bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
            var hashedPlainPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            return hashedPlainPassword == hashedPassword;
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] LoginDTO model)
        {

            if (await _dbContext.Accounts
                .FirstOrDefaultAsync(x => x.UserName == model.UserName) != null)
            {
                return BadRequest("Username already exists. Please choose a different username.");
            }

            // Create a new Account instance
            var newUser = new Account
            {
                UserName = model.UserName,
                UserPassword = model.UserPassword,
                Role = 1, // Set the desired role
                Status = true // Set the desired status
            };

            // Add the new user to the database
            _dbContext.Accounts.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok("Registration successful!");
        }


        //Method to Generate JWT Token
        public static string GenerateJwtToken(string userName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("phchoylleellpsnvdcvyaehkyhnqjzwchsrintkzzyycoscbbskfezuhbzhihntpsswkkzleodbugovoikuqcwwrxhqlpcfofkrhysvqdzvvbkoxwlasrpgcgncepozi")); // Replace with your actual secret key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = "your_issuer"; // Set your issuer
            var audience = "your_audience"; // Set your audience
            var jwtValidity = DateTime.Now.AddMinutes(60); // Set token validity (1 hour)

            var token = new JwtSecurityToken(
                issuer,
                audience,
                new[]
                {
            new Claim(ClaimTypes.Name, userName) // Add any other claims you need
                },
                expires: jwtValidity,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }


}
