using Microsoft.AspNetCore.Mvc;
using MVC_WaterBilling_API.Data.UsersData;

namespace MVC_WaterBilling_API.Controllers.UsersController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashierController : Controller
    {

        private readonly CashierData _cashierData;

        public CashierController(CashierData cashierData)
        {
            _cashierData = cashierData;
        }

        

        
    }
}
