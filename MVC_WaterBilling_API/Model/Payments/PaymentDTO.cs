using MVC_WaterBilling_API.Model.Bill;
using MVC_WaterBilling_API.Model.Consumer;
using MVC_WaterBilling_API.Model.Meter_Reading;
using MVC_WaterBilling_API.Model.User;
using System.ComponentModel.DataAnnotations;

namespace MVC_WaterBilling_API.Model.Payments
{
    public class PaymentDTO
    {
        [Required(ErrorMessage = "Bill ID is required!")]
        public int BillID { get; set; }

        [Required(ErrorMessage = "Casher ID is required!")]
        public string CashierID { get; set; } //From User Role (Cashier)

        [Required(ErrorMessage = "Amount Paid is required!")]
        public double Amount_Paid { get; set; }

        [Required(ErrorMessage = "Change is required!")]
        public double Change { get; set; }

        public double PenaltyIncluded { get; set; } = 0;
        public string? AdvanceIncluded { get; set; } //Insert Advance ID from advance table


        [Required(ErrorMessage = "Payment method is required!")]
        public string PaymentMethod { get; set; } //Bank Transfer, Cash, GCash
        
        public string? Remarks { get; set; }
        public string? paymentReferenceNumber { get; set; }
    }

    public class PaymentsWithUserConsumer
    {
        public Consumers Consumer { get; set; }
        public Users User { get; set; }
        public MeterReading MeterReading { get; set; }
        public Bills Bills { get; set; }
        public Payments Payments { get; set; }
    }

}
