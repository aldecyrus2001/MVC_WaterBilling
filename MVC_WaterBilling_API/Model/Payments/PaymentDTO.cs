using System.ComponentModel.DataAnnotations;

namespace MVC_WaterBilling_API.Model.Payments
{
    public class PaymentDTO
    {
        [Required(ErrorMessage = "Bill ID is required!")]
        public string BillID { get; set; }

        [Required(ErrorMessage = "Casher ID is required!")]
        public string CashierID { get; set; } //From User Role (Cashier)

        [Required(ErrorMessage = "Amount Paid is required!")]
        public double Amount_Paid { get; set; }

        [Required(ErrorMessage = "Change is required!")]
        public double Change { get; set; }

        public double PenaltyIncluded { get; set; } = 0;
        public string? AdvanceIncluded { get; set; } //Insert Advance ID from advance table

        [Required(ErrorMessage = "Payment Date is required!")]
        public DateOnly PaymentDate { get; set; }

        [Required(ErrorMessage = "Payment method is required!")]
        public string PaymentMethod { get; set; } //Bank Transfer, Cash, GCash
        
        public string? Remarks { get; set; }
    }
}
