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

        public async Task<List<MeterReading>> GetMeterReadingsAsync()
        {
            return await _db.Meters.OrderByDescending(m => m.Reading_Date).ToListAsync();
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



        public async Task CreateMeterReadingAsync(MeterReading meterReading)
        {
            _db.Meters.Add(meterReading);
            await _db.SaveChangesAsync();
        }



    }
}
