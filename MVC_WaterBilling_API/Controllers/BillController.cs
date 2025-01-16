using Microsoft.AspNetCore.Mvc;
using MVC_WaterBilling_API.Data;
using MVC_WaterBilling_API.Model.Bill;

namespace MVC_WaterBilling_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : Controller
    {
        private readonly BillData _billData;

        public BillController(BillData billData)
        {
            _billData = billData;
        }

        [HttpGet]
        public async Task<IActionResult> GetBills()
        {
            var bills = await _billData.GetBillAsync();
            return Ok(bills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBill(int id)
        {
            var bill = await _billData.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            return Ok(bill);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBill([FromBody] BillDTO billDTO)
        {
            var bills = new Bills
            {
                ReadingID = billDTO.ReadingID,
                Consumed_Amount = billDTO.Consumed_Amount,
                From = billDTO.From,
                To = billDTO.To,
                DueDate = billDTO.DueDate,
                BillDate = billDTO.BillDate,
                BillStatus = "Unpaid"
            };

            await _billData.CreatePaymentAsync(bills);

            return Ok(new
            {
                message = "Bill inserted successfully!"
            });
        }

    }
}
