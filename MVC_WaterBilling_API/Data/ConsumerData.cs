using Microsoft.EntityFrameworkCore;
using MVC_WaterBilling_API.Model.Consumer;
using MVC_WaterBilling_API.Model.Meter_Reading;
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

        public async Task<List<ConsumerWithUser>> GetConsumersWithUsersAsync()
        {
            var result = await (from c in _db.Consumers
                                where c.Consumer_Status != "Disconnected"
                                join u in _db.Users on c.UserID equals u.UserID.ToString()
                                select new ConsumerWithUser
                                {
                                    Consumer = c,
                                    User = u
                                }).ToListAsync();
            return result;
        }


        public async Task<Consumers?> GetConsumerByIdAsync(int id)
        {
            return await _db.Consumers.FindAsync(id);
        }

        public async Task<ConsumerWithUser?> GetConsumerWithUserAsync(int consumerId)
        {
            var result = await (from c in _db.Consumers
                                join u in _db.Users on c.UserID equals u.UserID.ToString()
                                where c.ConsumerID == consumerId
                                select new ConsumerWithUser
                                {
                                    Consumer = c,
                                    User = u
                                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Consumers?> GetConsumerByMeterNumberAsync(string meterNumber)
        {
            return await _db.Consumers
                .Where(c => c.Meter_Number == meterNumber)
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

        public async Task CreateConsumerAsync(Consumers consumers, MeterReading meterReading)
        {
            _db.Consumers.Add(consumers);
            _db.Meters.Add(meterReading);
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

        public async Task<List<ConsumerWithUser>> SearchConsumerAsync(string searchQuery)
        {
            var query = from c in _db.Consumers
                        join u in _db.Users on c.UserID equals u.UserID.ToString()
                        where u.Status != "Deleted"
                        select new ConsumerWithUser
                        {
                            Consumer = c,
                            User = u
                        };

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.ToLower().Trim(); // Normalize input

                query = query.Where(cu =>
                    cu.User.UserID.ToString().Contains(searchQuery) ||
                    cu.User.Firstname.ToLower().Contains(searchQuery) ||
                    cu.User.Middlename.ToLower().Contains(searchQuery) ||
                    cu.User.Lastname.ToLower().Contains(searchQuery) ||
                    cu.User.Gender.ToLower().Contains(searchQuery) ||
                    cu.User.PhoneNumber.Contains(searchQuery) ||
                    cu.User.Email.ToLower().Contains(searchQuery) ||
                    cu.User.Role.ToLower().Contains(searchQuery) ||
                    cu.Consumer.ConsumerID.ToString().Contains(searchQuery) || // Search in Consumer
                    cu.Consumer.Meter_Number.Contains(searchQuery) ||
                    cu.Consumer.Address.ToLower().Contains(searchQuery)
                );
            }

            return await query.OrderByDescending(cu => cu.User.UserID).ToListAsync();
        }

    }

}
