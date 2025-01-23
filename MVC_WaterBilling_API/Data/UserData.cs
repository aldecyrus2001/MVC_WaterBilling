using Microsoft.EntityFrameworkCore;
using MVC_WaterBilling_API.Model.User;
using MVC_WaterBilling_API.Services;

namespace MVC_WaterBilling_API.Data
{
    public class UserData
    {
        private readonly ApplicationDBContext _db;

        public UserData(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<List<Users>> GetUsersAsync()
        {
            return await _db.Users.Where(u => u.Status != "Deleted").OrderByDescending(u => u.UserID).ToListAsync();
        }

        public async Task<IEnumerable<Users>> GetUsersByRoleAsync(string role)
        {
            return await _db.Users
                .Where(u => u.Role == role && u.Status != "Deleted")
                .ToListAsync();
        }

        public async Task<Users?> GetUserByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<bool> IsEmailUsedAsync(string email)
        {
            return await _db.Users.AnyAsync(u => u.Email == email);
        }

        public async Task CreateUserAsync(Users user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Users user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task MarkUserAsDeletedAsync(Users user)
        {
            user.Status = "Deleted";
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
