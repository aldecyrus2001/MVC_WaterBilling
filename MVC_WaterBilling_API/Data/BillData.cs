using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_WaterBilling_API.Model.Bill;
using MVC_WaterBilling_API.Model.Meter_Reading;
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

        public async Task<List<Bills>> GetBillAsync()
        {
            return await _db.Bills.OrderByDescending(b=> b.BillID).ToListAsync();
        }

        public async Task<List<BillingReadingConsumerUser>> GetBillByConsumersIDAsync(int UserId)
        {
            var result = await (from b in _db.Bills
                                where b.BillStatus == "Unpaid" || b.BillStatus == "Declined"
                                join r in _db.Meters on b.ReadingID equals r.ReadingID into readingGroup
                                from reading in readingGroup.DefaultIfEmpty()

                                join c in _db.Consumers on reading.Meter_Number equals c.Meter_Number into consumerGroup
                                from consumer in consumerGroup.DefaultIfEmpty()

                                join u in _db.Users on consumer.UserID equals u.UserID.ToString() into userGroup
                                from user in userGroup.DefaultIfEmpty()

                                where user != null && user.UserID == UserId
                                orderby b.BillID descending

                                select new BillingReadingConsumerUser
                                {
                                    billing = b,
                                    meterReading = reading,
                                    consumers = consumer,
                                    users = user
                                }).ToListAsync();

            return result;
        }

        public async Task<Bills?> GetBillByReadingIdAsync(int readingId)
        {
            return await _db.Bills.FirstOrDefaultAsync(b => b.ReadingID == readingId);
        }

        public async Task<Bills?> GetBillByIdAsync(int id)
        {
            return await _db.Bills.FindAsync(id);
        }

        public async Task<Bills?> GetBillByReferenceNumberAsync(string referenceNumber)
        {
            return await _db.Bills
                .Where(m => m.ReferenceNumber == referenceNumber)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreatePaymentAsync(Bills bills)
        {
            bool exists = await _db.Bills
               .AnyAsync(b => b.ReferenceNumber == bills.ReferenceNumber);
            if (exists)
            {
                return false; // Reject insertion
            }

            _db.Bills.Add(bills);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task UpdateBillStatus(Bills bill)
        {
            _db.Bills.Attach(bill);
            _db.Entry(bill).Property(x => x.BillStatus).IsModified = true;
            await _db.SaveChangesAsync();
        }

        public async Task<bool> DeleteBillAsync(int Id)
        {
            var billing = await _db.Bills.FirstOrDefaultAsync(b => b.ReadingID == Id);

            if (billing == null)
            {
                return false;
            }

            _db.Bills.Remove(billing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Bills>> SearchAsync(string searchQuery)
        {
            var query = _db.Bills.OrderByDescending(b => b.BillID).AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.ToLower().Trim();

                DateTime searchDate;
                bool isDate = DateTime.TryParse(searchQuery, out searchDate);

                query = query.Where(b =>
                    b.ReadingID.ToString().Contains(searchQuery)||
                    b.BillID.ToString().Contains(searchQuery)||
                    b.ReferenceNumber.Contains(searchQuery) ||
                    b.Consumed_Amount.ToString().Contains(searchQuery) ||
                    b.BillStatus.Contains(searchQuery) ||
                    (isDate && (b.BillDate.Date == searchDate.Date ||
                                b.From == DateOnly.FromDateTime(searchDate) ||
                                b.To == DateOnly.FromDateTime(searchDate) ||
                                b.DueDate == DateOnly.FromDateTime(searchDate)))
                );
            }

            return await query.OrderByDescending(b => b.BillDate).ToListAsync();
        }


    }
}
