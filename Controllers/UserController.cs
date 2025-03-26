using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using PetsAPI.Data;
using PetsAPI.Model;
using PetsAPI.Viewmodel;
using System.Text.Json.Nodes;
namespace PetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Petsdb_context _context;


        public UserController(Petsdb_context context)
        {
            _context = context;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginRequest jvalue)
        {
            if(jvalue== null)
            {

                return BadRequest();
            }
            string username = jvalue.UserName.ToString();
            string password = jvalue.Password.ToString();

            var datauser = _context.UserRegistration.FirstOrDefault(x => x.UserName == username && x.Password == password);
            if(datauser == null)
            {
                return BadRequest();

            }
            return Ok(new { success = true, message = "logged in successfully", userid = datauser.UserID });

        }
        [HttpPost("UserRegistration")] // Explicit route added

        public async Task<IActionResult> UserRegistration([FromBody] UserRegistration userDetails)
        {
            try
            {
                if(userDetails != null)
                {
                    var user = new UserRegistration
                    {
                        UserName = userDetails.UserName,
                        UserEmailID = userDetails.UserEmailID,
                        UserPhoneNumber = userDetails.UserPhoneNumber,
                        Password = userDetails.Password,
                        CreateOn = DateTime.UtcNow,
                        Status = true,
                        UserPhoto = "",

                    };

                    // 6. Save to database
                    _context.UserRegistration.Add(user);
                    await _context.SaveChangesAsync();

                    return Ok(new
                    {
                        Success = true,
                        Message = "User registered successfully",
                    
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok();
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var userdata =_context.UserRegistration.Where(x => x.Status);

            return Ok(new { success = true, userdata });
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                // First find the user to delete
                var user = await _context.UserRegistration.FindAsync(userId);

                if (user == null)
                {
                    return NotFound(new { success = false, message = "User not found" });
                }

                // Remove the user
                _context.UserRegistration.Remove(user);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Deleted Successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception (you should have proper logging here)
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the user" });
            }
        }
    }
}
