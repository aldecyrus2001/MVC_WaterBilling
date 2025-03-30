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
        private readonly MeterReadingData _meterData;
        private readonly PaymentData _paymentData;

        public BillController(BillData billData, MeterReadingData meterData, PaymentData paymentData)
        {
            _billData = billData;
            _meterData = meterData;
            _paymentData = paymentData;
        }

        [HttpGet]
        public async Task<IActionResult> GetBills()
        {
            var bills = await _billData.GetBillAsync();
            return Ok(bills);
        }

        [HttpGet("Consumer/Billings/{UserId}")]
        public async Task<IActionResult> GetBills(int UserId)
        {
            var bills = await _billData.GetBillByConsumersIDAsync(UserId);
            return Ok(bills);
        }



        [HttpGet("{id}/ReadingID")]
        public async Task<IActionResult> GetBillByReading(int id)
        {
            var bill = await _billData.GetBillByReadingIdAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            return Ok(bill);
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

        [HttpGet("{referenceNumber}/reference")]
        public async Task<IActionResult> GetMEterReadingByReferenceNumber(string referenceNumber)
        {
            var meterReading = await _billData.GetBillByReferenceNumberAsync(referenceNumber);
            if (meterReading == null)
            {
                return NotFound();
            }

            return Ok(meterReading);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBill([FromBody] BillDTO billDTO)
        {
            var bills = new Bills
            {
                ReadingID = billDTO.ReadingID,
                ReferenceNumber = billDTO.ReferenceNumber,
                Consumed_Amount = billDTO.Consumed_Amount,
                From = billDTO.From,
                To = billDTO.To,
                DueDate = billDTO.DueDate,
                BillDate = DateTime.Now,
                BillStatus = "Unpaid"
            };

            bool isInserted = await _billData.CreatePaymentAsync(bills);
            if (!isInserted)
            {
                return BadRequest(new
                {
                    message = "Invalid Action, Bill Reference Number Has Been Used!"
                });
            }
            else
            {
                return Ok(new
                {
                    message = "Bill inserted successfully!",
                    billID = bills.BillID
                });
            }
            
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> EditBillingStatus(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Billing ID is invalid!" });
            }

            var bill = await _billData.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound(new { message = $"Bill with ID {id} not found!" });
            }

            var reading = await _meterData.GetReadingByIdAsync(bill.ReadingID);
            if (reading == null)
            {
                return NotFound(new { message = $"Reading with ID {id} not found!" });
            }

            bill.BillStatus = "Paid";
            reading.Status = "Paid";

            await _meterData.UpdateReadingStatus(reading);
            await _billData.UpdateBillStatus(bill);

            return Ok(new { message = "Billing status updated successfully!" });
        }

        [HttpPut("Consumers/{id}")]
        public async Task<IActionResult> EditBillingStatusFromConsumer(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Billing ID is invalid!" });
            }

            var bill = await _billData.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound(new { message = $"Bill with ID {id} not found!" });
            }

            var reading = await _meterData.GetReadingByIdAsync(bill.ReadingID);
            if (reading == null)
            {
                return NotFound(new { message = $"Reading with ID {id} not found!" });
            }

            

            bill.BillStatus = "Pending";
            reading.Status = "Pending";

            await _meterData.UpdateReadingStatus(reading);
            await _billData.UpdateBillStatus(bill);

            return Ok(new { message = "Billing status updated successfully!" });
        }

        [HttpPut("Consumers/{billID}/{Status}")]
        public async Task<IActionResult> UpdateBillingStatus(int billID, string Status)
        {
            if (billID <= 0)
            {
                return BadRequest(new { message = "Billing ID is invalid!" });
            }

            var bill = await _billData.GetBillByIdAsync(billID);
            if (bill == null)
            {
                return NotFound(new { message = $"Bill with ID {billID} not found!" });
            }

            var reading = await _meterData.GetReadingByIdAsync(bill.ReadingID);
            if (reading == null)
            {
                return NotFound(new { message = $"Reading with ID {billID} not found!" });
            }

            var payment = await _paymentData.GetPaymentByBillIDAsync(bill.BillID);
            if (payment == null)
            {
                return NotFound(new { message = $"Payment with ID {bill.BillID} not found!" });
            }

            bill.BillStatus = Status;
            reading.Status = Status;
            payment.Remarks = Status;

            await _meterData.UpdateReadingStatus(reading);
            await _billData.UpdateBillStatus(bill);
            await _paymentData.UpdatePaymentRemarks(payment);

            return Ok(new { message = "Billing status updated successfully!" });
        }

        [HttpDelete("{id}/Delete")]
        public async Task<bool> DeleteBill(int Id)
        {
            var billing = await _billData.GetBillByReadingIdAsync(Id);

            if (billing == null)
            {
                return false;
            }

            await _billData.DeleteBillAsync(Id);

            return true;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string? search = null)
        {
            var data = await _billData.SearchAsync(search);
            return Ok(data);
        }

    }
}
