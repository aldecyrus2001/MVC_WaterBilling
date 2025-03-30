namespace MVC_WaterBilling_API.Model.Payments
{
    public class Payments
    {
        public int PaymentID { get; set; }
        public int BillID { get; set; }
        public string CashierID { get; set; } //From User Role (Cashier)
        public double Amount_Paid { get; set; }
        public double Change { get; set; }
        public double PenaltyIncluded { get; set; } = 0; //Get From Penalty from Settings table
        public string? AdvanceIncluded { get; set; } //Insert Advance ID from advance table
        public DateOnly PaymentDate { get; set; }
        public string PaymentMethod { get; set; } //Bank Transfer, Cash, GCash
        public string? Remarks { get; set; }
        public string? PaymentReferenceNumber { get; set; }
    }
}
