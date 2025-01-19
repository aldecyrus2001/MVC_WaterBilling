using MVC_WaterBilling_API.Services;

namespace MVC_WaterBilling_API.Data.UsersData
{
    public class CashierData
    {
        private readonly ApplicationDBContext _db;

        public CashierData(ApplicationDBContext db)
        {
            _db = db;
        }

        
    }
}
