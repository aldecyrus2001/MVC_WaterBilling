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
                Middlename = userDTO.Middlename,
                Lastname = userDTO.Lastname,
                Gender = userDTO.Gender,
                PhoneNumber = userDTO.PhoneNumber,
                Email = userDTO.Email,
                Password = hashedPassword,
                Date_Created = DateTime.Now,
                Status = userDTO.Status,
                Role = userDTO.Role,
            };

            await _userData.CreateUserAsync(user);

            return Ok(new { message = "User created successfully!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(int id, [FromBody] UsersDTO userDTO)
        {
            var user = await _userData.GetUserByIdAs
