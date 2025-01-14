using Microsoft.EntityFrameworkCore;
using MVC_WaterBilling_API.Model.Consumer;
using MVC_WaterBilling_API.Services;

namespace MVC_WaterBilling_API.Data
{
    public class ConsumerData
    {
        public readonly ApplicationDBContext _db;

        public ConsumerData(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<List<Consumers>> GetConsumersAsync()
        {
            return await _db.Consumers
                .Where(c => c.Consumer_Status != "Disconnected")
                .OrderByDescending(c => c.ConsumerID)
                .ToListAsync();
        }

        public async Task<Consumers?> GetConsumerByIdAsync(int id)
        {
            return await _db.Consumers.FindAsync(id);
        }

        public async Task<bool> isUserIdUsedAsync(string UserID)
        {
            return await _db.Consumers.AnyAsync(c => c.UserID == UserID);
        }

        public async Task CreateConsumerAsync(Consumers consumers)
        {
            _db.Consumers.Add(consumers);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateConsumerAsync(Consumers consumers)
        {
            _db.Consumers.Update(consumers);
            await _db.SaveChangesAsync();
        }

        public async Task DisconnectConsumerAsync(Consumers consumers)
        {
            consumers.Consumer_Status = "Disconnected";
            _db.Consumers.Update(consumers);
            await _db.SaveChangesAsync();
        }
    }
}
