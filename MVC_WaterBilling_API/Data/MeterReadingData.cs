using Microsoft.EntityFrameworkCore;
using MVC_WaterBilling_API.Model.Meter_Reading;
using MVC_WaterBilling_API.Services;

namespace MVC_WaterBilling_API.Data
{
    public class MeterReadingData
    {
        private readonly ApplicationDBContext _db;

        public MeterReadingData(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<MeterReading?> GetReadingByIdAsync(int readingId)
        {
            return await _db.Meters.FirstOrDefaultAsync(m => m.ReadingID == readingId);
        }

        public async Task<List<ReadingConsumerUserDTO>> GetMeterReadingsAsync()
        {
            var result = await (from m in _db.Meters // Start with MeterReadings
                                join c in _db.Consumers on m.Meter_Number equals c.Meter_Number into consumerGroup
                                from consumer in consumerGroup.DefaultIfEmpty() // Left Join Consumers
                                join u in _db.Users on consumer.UserID equals u.UserID.ToString() into userGroup
                                from user in userGroup.DefaultIfEmpty() // Left Join Users
                                select new ReadingConsumerUserDTO
                                {
                                    MeterReading = m,
                                    Consumers = consumer,
                                    Users = user
                                }).ToListAsync();

            return result;
        }



        public async Task<MeterReading?> GetMeterReadingByMeterNumberAsync(string meterNumber)
        {
            return await _db.Meters
                .Where(m => m.Meter_Number == meterNumber)
                .OrderByDescending(m => m.Reading_Date) // Order by latest reading date
                .FirstOrDefaultAsync(); // Get the first (and therefore latest) reading
        }

        public async Task<ReadingConsumerUserDTO?> ViewMeterReadingsAsync(int id)
        {
            var result = await (from m in _db.Meters // Start with MeterReadings
                                join c in _db.Consumers on m.Meter_Number equals c.Meter_Number into consumerGroup
                                from consumer in consumerGroup.DefaultIfEmpty() // Left Join Consumers
                                join u in _db.Users on consumer.UserID equals u.UserID.ToString() into userGroup
                                from user in userGroup.DefaultIfEmpty() // Left Join Users
                                select new ReadingConsumerUserDTO
                                {
                                    MeterReading = m,
                                    Consumers = consumer,
                                    Users = user
                                }).FirstOrDefaultAsync();

            return result;
        }


        public async Task<MeterReading?> GetMeterReadingsAsync(int id)
        {
            return await _db.Meters.FindAsync(id);
        }


        public async Task<MeterReading?> GetPreviousReadingAsync(string meterNumber)
        {
            return await _db.Meters
                .Where(m => m.Meter_Number == meterNumber)
                .OrderByDescending(m => m.Reading_Date)
                .FirstOrDefaultAsync();
        }



        public async Task<bool> CreateMeterReadingAsync(MeterReading meterReading)
        {
            bool exists = await _db.Meters
                .AnyAsync(m => m.Meter_Number == meterReading.Meter_Number &&
                m.MonthOf == meterReading.MonthOf);

            if (exists)
            {
                return false; // Reject insertion
            }

            _db.Meters.Add(meterReading);
            await _db.SaveChangesAsync();
            return true; // Insertion successful
        }

        public async Task<bool> DeleteMeterReadingAsync(int Id)
        {
            var meterReading = await _db.Meters.FirstOrDefaultAsync(m => m.ReadingID == Id);

            if (meterReading == null)
            {
                return false;
            }

            _db.Meters.Remove(meterReading);
            await _db.SaveChangesAsync(); // Save changes to apply deletion
            return true; // Deletion successful
        }

        public async Task UpdateReadingStatus(MeterReading reading)
        {
            _db.Meters.Attach(reading);
            _db.Entry(reading).Property(r => r.Status).IsModified = true;
            await _db.SaveChangesAsync();
        }

        public async Task<List<ReadingConsumerUserDTO>> SearchAsync(string searchQuery)
        {
            var query = from m in _db.Meters // Start with MeterReadings
                        join c in _db.Consumers on m.Meter_Number equals c.Meter_Number into consumerGroup
                        from consumer in consumerGroup.DefaultIfEmpty() // Left Join Consumers
                        join u in _db.Users on consumer.UserID equals u.UserID.ToString() into userGroup
                        from user in userGroup.DefaultIfEmpty() // Left Join Users
                        where user == null || user.Status != "Deleted" // Exclude Deleted Users
                        select new ReadingConsumerUserDTO
                        {
                            MeterReading = m,
                            Consumers = consumer,
                            Users = user
                        };

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.ToLower().Trim(); // Normalize input

                // Try parsing the searchQuery as a DateTime
                DateTime searchDate;
                bool isDate = DateTime.TryParse(searchQuery, out searchDate);

                query = query.Where(rcu =>
                    rcu.Users.Firstname.ToLower().Contains(searchQuery) ||
                    rcu.Users.Middlename.ToLower().Contains(searchQuery) ||
                    rcu.Users.Lastname.ToLower().Contains(searchQuery) ||
                    rcu.Users.Gender.ToLower().Contains(searchQuery) ||
                    rcu.Users.PhoneNumber.Contains(searchQuery) ||
                    rcu.Users.Email.ToLower().Contains(searchQuery) ||
                    rcu.Users.Role.ToLower().Contains(searchQuery) ||
                    rcu.Consumers.Address.ToLower().Contains(searchQuery) ||
                    rcu.Consumers.ConnectionType.ToLower().Contains(searchQuery) ||
                    rcu.Consumers.Meter_Number.Contains(searchQuery) ||
                    rcu.Consumers.Consumer_Status.ToLower().Contains(searchQuery) ||
                    rcu.MeterReading.ReaderID.Contains(searchQuery) ||
                    rcu.MeterReading.Previous_Reading.ToString().Contains(searchQuery) ||
                    rcu.MeterReading.Current_Reading.ToString().Contains(searchQuery) ||
                    rcu.MeterReading.Usage.ToString().Contains(searchQuery) ||
                    rcu.MeterReading.Status.ToLower().Contains(searchQuery) ||
                    (rcu.MeterReading.MonthOf != null && rcu.MeterReading.MonthOf.ToLower().Contains(searchQuery)) ||
                    (isDate && rcu.MeterReading.Reading_Date.Date == searchDate.Date) // Compare as DateTime
                );
            }

            return await query.OrderByDescending(rcu => rcu.MeterReading.Reading_Date).ToListAsync();
        }

        public async Task<int> GetCountByReader(string readerId)
        {
            return await _db.Meters.CountAsync(m => m.ReaderID == readerId);
        }



    }
}
