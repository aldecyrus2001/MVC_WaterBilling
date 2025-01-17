using Microsoft.EntityFrameworkCore;
using MVC_WaterBilling_API.Model.Payments;
using MVC_WaterBilling_API.Services;

namespace MVC_WaterBilling_API.Data
{
    public class PaymentData
    {
        private readonly ApplicationDBContext _db;

        public PaymentData(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<List<Payments>> GetPaymentsAsync()
        {
            return await _db.Payments.OrderByDescending(p => p.PaymentID).ToListAsync();
        }

        public async Task<Payments?> GetPaymentByPaymentIDAsync(int id)
        {
            return await _db.Payments.FindAsync(id);
        }

        public async Task<bool> isBilledAlreadyAsync(string BillID)
        {
            return await _db.Payments.AnyAsync(p => p.BillID == BillID);
        }

        public async Task CreatePaymentAsync(Payments payments)
        {
            _db.Payments.Add(payments);
            await _db.SaveChangesAsync();
        }
    }

}
