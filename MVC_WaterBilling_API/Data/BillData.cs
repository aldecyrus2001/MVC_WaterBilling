using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_WaterBilling_API.Model.Bill;
using MVC_WaterBilling_API.Model.Payments;
using MVC_WaterBilling_API.Services;

namespace MVC_WaterBilling_API.Data
{
    public class BillData
    {
        private readonly ApplicationDBContext _db;

        public BillData(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<List<BillWithReadingConsumerUser>> GetBillAsync()
        {
            return await _db.Bills
                .Where(b => b.BillStatus == "Unpaid")
                .OrderByDescending(b => b.BillID)
                .Join(
                    _db.Meters,
                    bill => bill.ReadingID,
                    reading => reading.ReadingID.ToString(),
                    (bill, reading) => new { bill, reading }
                )
                .Join(
                    _db.Consumers,
                    combined => combined.reading.Meter_Number,
                    consumer => consumer.Meter_Number,
                    (combined, consumer) => new { combined.bill, combined.reading, consumer }
                )
                .Join(
                    _db.Users,
                    combined => combined.consumer.UserID,
                    user => user.UserID.ToString(),
                    (combined, user) => new BillWithReadingConsumerUser
                    {
                        MeterReading = combined.reading,
                        Consumers = combined.consumer,
                        Users = user
                    }
                )
                .ToListAsync();
        }



        public async Task<BillWithReadingConsumerUser?> GetBillByIdAsync(int id)
        {
            return await _db.Bills
                .Where(b => b.BillID == id)
                .Join(
                    _db.Meters,
                    bill => bill.ReadingID,
                    reading => reading.ReadingID.ToString(),
                    (bill, reading) => new { bill, reading }
                )
                .Join(
                    _db.Consumers,
                    combined => combined.reading.Meter_Number,
                    consumer => consumer.Meter_Number,
                    (combined, consumer) => new { combined.bill, combined.reading, consumer }
                )
                .Join(
                    _db.Users,
                    combined => combined.consumer.UserID,
                    user => user.UserID.ToString(),
                    (combined, user) => new BillWithReadingConsumerUser
                    {
                        MeterReading = combined.reading,
                        Consumers = combined.consumer,
                        Users = user
                    }
                )
            .FirstOrDefaultAsync();
        }

        public async Task CreatePaymentAsync(Bills bills)
        {
            _db.Bills.Add(bills);
            await _db.SaveChangesAsync();
        }

    }
}
