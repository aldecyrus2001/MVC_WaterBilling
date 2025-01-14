using Microsoft.EntityFrameworkCore;
using MVC_WaterBilling_API.Model.User;
using MVC_WaterBilling_API.Services;
using System.Security.Cryptography;

namespace MVC_WaterBilling_API.Data
{
    public class AuthenticationData
    {
        private readonly ApplicationDBContext _db;

        public AuthenticationData(ApplicationDBContext db)
        {
            _db = db;
        }

        // Fetch user by email
        public async Task<Users> GetUserByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email && u.Status != "Deleted");
        }

        // Update user token
        public async Task UpdateUserTokenAsync(Users user, string token)
        {
            user.Token = token; // Assuming Users model has a Token property
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }

        // Generate a secure random token
        public string GenerateSecureToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }
    }
}
