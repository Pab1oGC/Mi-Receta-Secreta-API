using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using MiRecetaSecretaAPI.Data;
using MiRecetaSecretaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MiRecetaSecretaAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UsersContoller : Controller
    {
        private readonly AppDBContext _db;
        public UsersContoller(AppDBContext appDBContext)
        {
            _db = appDBContext;
           
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if(!ModelState.IsValid)
    {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { message = "Validation failed", errors });
            }

            if (user.Rol?.ToLower() != "client")
            {
                return Conflict(new { message = "Invalid role. Only 'client' is allowed." });
            }
            var existingUser = await _db.User.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return Conflict(new { message = "The email is already in use." });
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Status = 1;
            user.CreatedBy = 1;
            _db.User.Add(user);
            await _db.SaveChangesAsync();

            return Ok(new { message = "User created successfully", userId = user.Id });
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _db.User.Where(u => u.Status == 1).ToListAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _db.User.FirstOrDefaultAsync(u => u.Id == id && u.Status == 1);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var existingUser = await _db.User.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Names = user.Names;
            existingUser.Lastname = user.Lastname;
            existingUser.Email = user.Email;
            existingUser.Rol = user.Rol;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error updating user.");
            }

            return Ok(existingUser);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _db.User.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            user.Status = 0; 
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the user" });
            }

            return Ok(new { message = "User deleted successfully" });
        }
    }
}
