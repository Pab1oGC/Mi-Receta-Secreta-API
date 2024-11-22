using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiRecetaSecretaAPI.Data;
using System.Security.Claims;
using MiRecetaSecretaAPI.Models;


namespace MiRecetaSecretaAPI.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDBContext _dbContext;

        public AuthController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] MiRecetaSecretaAPI.Models.LoginRequest loginRequest)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });
      
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

            return Ok(new { message = "Login successful" });
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return Ok(new { message = "Logout successful" });
        }

        [HttpGet("denied")]
        public IActionResult AccessDenied()
        {
            return new ContentResult
            {
                StatusCode = StatusCodes.Status403Forbidden,
                Content = "Access denied"
            };
        }
    }
}
