namespace MVC_FrontEnd.Models
{
    public class Users
    {
        public int UserID { get; set; }
        public string Firstname { get; set; }
        public string? Middlename { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Date_Created { get; set; }
        public string Status { get; set; } //Deleted, Active
        public string Role { get; set; } //Administrator, Consumer, Cashier, FieldReader
        public string? Token { get; set; } //Used to access the website as Authorization
        public string? Applied { get; set; }
    }

    public class Consumers
    {
        public int ConsumerID { get; set; }
        public string UserID { get; set; }
        public string Address { get; set; }
        public string ConnectionType { get; set; } //Residential, Commercial
        public string Connection_Date { get; set; } //XXXX-XXXX-XXXX-XXXX
        public string Meter_Number { get; set; }
        public string Consumer_Status { get; set; } //Disconnected, Connected

        public Users User { get; set; }
    }

    public class Reading
    {
        public int ReadingID { get; set; } = 0;
        public string Meter_Number { get; set; }
        public string ReaderID { get; set; }
        public double Previous_Reading { get; set; }
        public double Current_Reading { get; set; }
        public double Usage { get; set; }
        public string Status { get; set; }
        public string MonthOf { get; set; }
        public DateTime Reading_Date { get; set; }
    }

    public class Bills
    {
        public int? BillID { get; set; }
        public string ReferenceNumber { get; set; }
        public int ReadingID { get; set; }
        public double Consumed_Amount { get; set; } //The total amount consumed by the consumers
        public DateOnly From { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly To { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateTime BillDate { get; set; } = DateTime.Today;
        public string BillStatus { get; set; } = string.Empty;//Paid, Unpaid

    }

    public class Advances
    {
        public string AdvanceID { get; set; }
        public string ConsumerID { get; set; }
        public double Amount { get; set; } // Advance Amount
        public DateTime DateInserted { get; set; }
        public DateTime? DateUsed { get; set; }
        public string Status { get; set; } //Used or Unused
        public double totalAmount { get; set; }
    }

    public class Payments
    {
        public int PaymentID { get; set; }
        public int? BillID { get; set; }
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

    public class Settings
    {
        public int SettingID { get; set; }
        public string? SystemName { get; set; }
        public double AmountPerCubic { get; set; }
        public double AmountPerCubicCommercial { get; set; }
        public byte[]? GcashQr { get; set; }
        public string? Gcash_Name { get; set; }
    }

    public class ConsumerWithUser
    {
        public Consumers Consumer { get; set; }
        public Users User { get; set; }
    }

    public class ReadingConsumerUser
    {
        public Users users { get; set; }
        public Consumers consumers { get; set; }
        public Reading meterReading { get; set; }
    }

    public class BillingReadingConsumerUser
    {
        public Users users { get; set; }
        public Consumers consumers { get; set; }
        public Reading meterReading { get; set; }
        public Bills billing { get; set; }
    }

    public class ReadingWithBilling
    {
        public Reading Reading { get; set; }
        public Bills Bills { get; set; }
    }

    public class PaymentsWithUserConsumer
    {
        public Payments payments { get; set; }
        public Consumers consumer { get; set; }
        public Users user { get; set; }
        public Reading meterReading { get; set; }
        public Bills bills { get; set; }
    }
}
