using Microsoft.AspNetCore.Mvc;
using MVC_WaterBilling_API.Data;
using MVC_WaterBilling_API.Model.Payments;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _paymentData.GetPaymentByPaymentIDAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);

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
                PaymentDate = paymentDTO.PaymentDate,
                PaymentMethod = paymentDTO.PaymentMethod,
                Remarks = paymentDTO.Remarks,
            };

            await _paymentData.CreatePaymentAsync(payment);
            return Ok(new
            {
                message = "Payment Created successfully!"
            });
        }
    }
}
