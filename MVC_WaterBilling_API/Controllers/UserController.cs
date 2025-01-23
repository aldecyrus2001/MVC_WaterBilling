using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_WaterBilling_API.Data;
using MVC_WaterBilling_API.Model.User;

namespace MVC_WaterBilling_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserData _userData;

        public UserController(UserData userData)
        {
            _userData = userData;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userData.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userData.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UsersDTO userDTO)
        {
            if (await _userData.IsEmailUsedAsync(userDTO.Email))
            {
                return BadRequest(new { message = "Email address already used." });
            }

            var passwordHasher = new PasswordHasher<Users>();
            var hashedPassword = passwordHasher.HashPassword(null, userDTO.Password);

            var user = new Users
            {
                Firstname = userDTO.Firstname,
                Middlename = userDTO.Middlename ?? string.Empty,
                Lastname = userDTO.Lastname,
                Gender = userDTO.Gender,
                PhoneNumber = userDTO.PhoneNumber,
                Email = userDTO.Email,
                Password = hashedPassword,
                Date_Created = DateTime.Now,
                Status = "Active",
                Role = userDTO.Role,
            };

            await _userData.CreateUserAsync(user);

            return Ok(new { message = "User created successfully!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(int id, [FromBody] UserUpdateDTO userDTO)
        {
            var user = await _userData.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (await _userData.IsEmailUsedAsync(userDTO.Email) && user.Email != userDTO.Email)
            {
                return BadRequest(new { message = "Email is already used." });
            }

            user.Firstname = userDTO.Firstname;
            user.Middlename = userDTO.Middlename;
            user.Lastname = userDTO.Lastname;
            user.Gender = userDTO.Gender;
            user.PhoneNumber = userDTO.PhoneNumber;
            user.Email = userDTO.Email;
            user.Role = userDTO.Role;

            await _userData.UpdateUserAsync(user);

            return Ok(new { message = "User updated successfully!" });
        }

        [HttpPut("{id}/delete")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userData.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userData.MarkUserAsDeletedAsync(user);

            return Ok(new { message = "User deleted successfully!" });
        }

        [HttpPut("reset-password/{id}")]
        public async Task<IActionResult> ResetPassword(int id, [FromBody] ChangePasswordRequest passwordRequest)
        {
            var user = await _userData.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var passwordHasher = new PasswordHasher<Users>();
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, passwordRequest.CurrentPassword);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Current password is incorrect.");
            }

            user.Password = passwordHasher.HashPassword(user, passwordRequest.NewPassword);
            await _userData.UpdateUserAsync(user);

            return Ok(new { message = "Password reset successfully." });
        }
    }
}
