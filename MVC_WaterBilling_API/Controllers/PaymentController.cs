using Microsoft.AspNetCore.Mvc;
using MVC_WaterBilling_API.Data;
using MVC_WaterBilling_API.Model.Payments;
using MVC_WaterBilling_API.Model.User;

namespace MVC_WaterBilling_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly PaymentData _paymentData;

        public PaymentController(PaymentData paymentData)
        {
            _paymentData = paymentData;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _paymentData.GetPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("Pending/Payments")]
        public async Task<IActionResult> GetPendingPayments()
        {
            var payments = await _paymentData.GetPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("ConsumersPayment/{UserID}")]
        public async Task<IActionResult> GetConsumersPayment(int UserID)
        {
            var payments = await _paymentData.GetConsumersPaymentAsync(UserID);
            return Ok(payments);
        }

        [HttpGet("CashierID/{CashierID}")]
        public async Task<IActionResult> GetCashierHistory(string CashierID)
        {
            var payments = await _paymentData.GetCashierHistory(CashierID);
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payments = await _paymentData.GetPaymentAsync(id);
            if (payments == null)
            {
                return NotFound();
            }

            return Ok(payments);
        }

        [HttpGet("OnlinePayments")]
        public async Task<IActionResult> GetOnlinePayments()
        {
            var payments = await _paymentData.GetOnlinePaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("totalAmount")]
        public async Task<IActionResult> GetPayment()
        {
            var totalAmount = await _paymentData.GetTodaytotalAmount();

            if (totalAmount == 0)
            {
                return Ok(totalAmount = 0);
            }

            return Ok(totalAmount);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentDTO paymentDTO)
        {
            if(await _paymentData.isBilledAlreadyAsync(paymentDTO.BillID))
            {
                return BadRequest(new
                {
                    message = "Bill has already recorded!"
                });
            }

            var payment = new Payments
            {
                BillID = paymentDTO.BillID,
                CashierID = paymentDTO.CashierID,
                Amount_Paid = paymentDTO.Amount_Paid,
                Change = paymentDTO.Change,
                PenaltyIncluded = paymentDTO.PenaltyIncluded,
                AdvanceIncluded = paymentDTO.AdvanceIncluded,
                PaymentDate = DateOnly.FromDateTime(DateTime.Now),
                PaymentMethod = paymentDTO.PaymentMethod,
                Remarks = paymentDTO.Remarks,
                PaymentReferenceNumber = paymentDTO.paymentReferenceNumber
            };

            await _paymentData.CreatePaymentAsync(payment);
            return Ok(new
            {
                message = "Payment Created successfully!"
            });
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string? search = null)
        {
            var data = await _paymentData.SearchAsync(search);
            return Ok(data);
        }

        [HttpGet("SearchPendings")]
        public async Task<IActionResult> SearchOnlinePayment([FromQuery] string? search = null)
        {
            var data = await _paymentData.SearchOnlinePaymentAsync(search);
            return Ok(data);
        }

        [HttpPut("{id}/{CashierID}")]
        public async Task<IActionResult> UpdatePayment(int id, string CashierID)
        {
            var payment = await _paymentData.GetPaymentByPaymentIDAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            await _paymentData.UpdatePaymentStatus(id, CashierID);

            return Ok(new
            {
                message = "Payment updated successfully!",
                billId = payment.BillID
            });
        }
    }
}
