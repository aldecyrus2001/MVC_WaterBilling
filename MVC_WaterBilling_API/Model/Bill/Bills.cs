using MVC_WaterBilling_API.Model.Consumer;
using MVC_WaterBilling_API.Model.Meter_Reading;
using MVC_WaterBilling_API.Model.User;

namespace MVC_WaterBilling_API.Model.Bill
{
    public class Bills
    {
        public int BillID { get; set; }
        public string ReferenceNumber { get; set; }
        public int ReadingID { get; set; }
        public double Consumed_Amount { get; set; } //The total amount consumed by the consumers
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
        public DateOnly DueDate { get; set; }
        public DateTime BillDate { get; set; }
        public string BillStatus { get; set; } //Paid, Unpaid
    }

    public class BillingReadingConsumerUser
    {
        public Users users { get; set; }
        public Consumers consumers { get; set; }
        public MeterReading meterReading { get; set; }
        public Bills billing { get; set; }
    }
}
