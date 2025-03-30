using Microsoft.EntityFrameworkCore;
using MVC_WaterBilling_API.Model.Advance;
using MVC_WaterBilling_API.Model.Payments;
using MVC_WaterBilling_API.Services;

namespace MVC_WaterBilling_API.Data
{
    public class AdvanceData
    {

        private readonly ApplicationDBContext _db;

        public AdvanceData(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<object> GetAdvancesAsync(string id)
        {
            var advances = await _db.Advances
                                    .Where(a => a.Status != "Used" && a.ConsumerID == id)
                                    .ToListAsync();

            var totalAmount = advances.Sum(a => a.Amount);

            return new
            {
                TotalAmount = totalAmount,
                Advances = advances
            };
        }

        public async Task<Advances> GetAdvanceAsync(int id)
        {
            return await _db.Advances.FirstOrDefaultAsync(a => a.AdvanceID == id);
        }

        public async Task CreateAdvanceAsync(Advances advances)
        {
            _db.Advances.Add(advances);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAdvanceStatusAsync(Advances advances)
        {
            _db.Advances.Update(advances);
            await _db.SaveChangesAsync();
        }

        
    }
}
