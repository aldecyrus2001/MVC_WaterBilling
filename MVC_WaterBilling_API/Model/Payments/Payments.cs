namespace MVC_WaterBilling_API.Model.Payments
{
    public class Payments
    {
        public int PaymentID { get; set; }
        public string BillID { get; set; }
        public string CashierID { get; set; } //From User Role (Cashier)
        public double Amount_Paid { get; set; }
        public double Change { get; set; }
        public string? PenaltyIncluded { get; set; } //Insert Penalty ID from penalty table
        public string? AdvanceIncluded { get; set; } //Insert Advance ID from advance table
        public DateOnly PaymentDate { get; set; }
        public string PaymentMethod { get; set; } //Bank Transfer, Cash, GCash
        public string? Remarks { get; set; }
    }
}
