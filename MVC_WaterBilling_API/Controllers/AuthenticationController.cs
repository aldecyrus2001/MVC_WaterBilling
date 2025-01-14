using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MVC_WaterBilling_API.Data;
using MVC_WaterBilling_API.Model.User;

namespace MVC_WaterBilling_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationData _authData;
        private readonly PasswordHasher<Users> _passwordHasher;

        public AuthenticationController(AuthenticationData authData)
        {
            _authData = authData;
            _passwordHasher = new PasswordHasher<Users>();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Email and Password Required!");
            }

            var user = await _authData.GetUserByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                return Unauthorized("Invalid Email or Password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginRequest.Password);
            if (result != PasswordVerificationResult.Success)
            {
                return Unauthorized("Invalid Email or Password");
            }

            var token = _authData.GenerateSecureToken();
            await _authData.UpdateUserTokenAsync(user, token);

            return Ok(new
            {
                message = "Login Successfully",
                userId = user.UserID,
                role = user.Role,
                token = token
            });
        }
    }
}
