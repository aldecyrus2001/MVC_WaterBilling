namespace MVC_WaterBilling_API.Model.Bill
{
    public class Bills
    {
        public int BillID { get; set; }
        public string ReadingID { get; set; }
        public double Consumed_Amount { get; set; } //The total amount consumed by the consumers
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime BillDate { get; set; }
        public string BillStatus { get; set; } //Paid, Unpaid

    }
}
