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

        public async Task<List<ConsumerWithUserDTO>> GetConsumersAsync()
        {
            return await _db.Consumers
                .Where(c => c.Consumer_Status != "Disconnected")
                .Join(
                    _db.Users,
                    consumer => consumer.UserID,
                    user => user.UserID.ToString(),
                    (consumer, user) => new ConsumerWithUserDTO
                    {
                        Consumer = consumer,
                        User = user
                    }
                )
                .OrderByDescending(c => c.Consumer.ConsumerID)
                .ToListAsync();
        }

        public async Task<ConsumerWithUserDTO?> GetConsumerByIdAsync(int id)
        {
            return await _db.Consumers
                .Where(c => c.ConsumerID == id)
                .Join(
                    _db.Users,
                    consumer => consumer.UserID,
                    user => user.UserID.ToString(),
                    (consumer, user) => new ConsumerWithUserDTO
                    {
                        Consumer = consumer,
                        User = user
                    }
                )
                .FirstOrDefaultAsync();
        }

        public async Task<bool> isUserIdUsedAsync(string UserID)
        {
            return await _db.Consumers.AnyAsync(c => c.UserID == UserID);
        }

        public async Task<bool> isMeterNumberUsedAsync(string MeterID)
        {
            return await _db.Consumers.AnyAsync(c => c.Meter_Number == MeterID);
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

        public async Task<object?> GetConsumerWithUser(string meterNumber)
        {
            return await _db.Consumers
                .Where(c => c.Meter_Number == meterNumber)
                .Join(
                    _db.Users,
                    consumer => consumer.UserID,
                    user => user.UserID.ToString(),
                    (consumer, user) => new
                    {
                        Consumer = consumer,
                        User = user
                    }
                )
                .FirstOrDefaultAsync();
        }

    }
}
