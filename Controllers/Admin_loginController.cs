using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetsAPI.Data;
using PetsAPI.Viewmodel;

namespace PetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Admin_loginController : ControllerBase
    {
        private readonly Petsdb_context _context;


        public Admin_loginController(Petsdb_context context)
        {
            _context = context;
        }


        [HttpPost("login")] // Use HttpPost and specify the route
        public async Task<IActionResult> Login([FromBody] AdminLoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid request. Username and password are required.");
            }

            var admin = await _context.Admin_Login.FirstOrDefaultAsync(a => a.UserName == request.UserName);

            if (admin == null)
            {
                return NotFound("Admin user not found."); // Return 404 if user not found
            }

            // In a real application, you should hash and salt passwords!
            if (admin.Password == request.Password)
            {
                return Ok(new { message = "Login successful." }); // Return 200 OK for success
            }
            else
            {
                return Unauthorized("Invalid password."); // Return 401 Unauthorized for incorrect password
            }
        }
    }
}