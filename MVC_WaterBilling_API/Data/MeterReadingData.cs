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

        public async Task<List<ReadingConsumerUserDTO>> GetMeterReadingsAsync()
        {
            return await _db.Meters
                .Join(
                    _db.Consumers, // Joining with Consumers
                    meterReading => meterReading.Meter_Number, // Key from MeterReading
                    consumer => consumer.Meter_Number, // Key from Consumers
                    (meterReading, consumer) => new { meterReading, consumer } // Result of first join
                )
                .Join(
                    _db.Users, // Joining with Users
                    mc => mc.consumer.UserID, // Key from Consumers (UserID)
                    user => user.UserID.ToString(), // Key from Users (UserID as string)
                    (mc, user) => new ReadingConsumerUserDTO // Select only the required fields
                    {
                        Users = user,           // Mapping user information
                        Consumers = mc.consumer, // Mapping consumer information
                        MeterReading = mc.meterReading // Mapping meter reading information
                    }
                )
                .OrderByDescending(m => m.MeterReading.Reading_Date) // Sort by Reading Date
                .ToListAsync();
        }


        public async Task<MeterReading?> GetMeterReadingByMeterNumberAsync(string meterNumber)
        {
            return await _db.Meters
                .Where(m => m.Meter_Number == meterNumber)
                .OrderByDescending(m => m.Reading_Date) // Order by latest reading date
                .FirstOrDefaultAsync(); // Get the first (and therefore latest) reading
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
            .AnyAsync(m => m.Meter_Number == meterReading.Meter_Number && m.MonthOf == meterReading.MonthOf);

            if (exists)
            {
                return false; // Reject insertion
            }

            _db.Meters.Add(meterReading);
            await _db.SaveChangesAsync();
            return true; // Insertion successful
        }



    }
}
