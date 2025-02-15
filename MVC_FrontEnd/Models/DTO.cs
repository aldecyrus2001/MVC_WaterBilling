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
        public int BillID { get; set; } = 0;
        public string ReadingID { get; set; } = string.Empty;
        public double Consumed_Amount { get; set; } = 0; //The total amount consumed by the consumers
        public DateOnly From { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly To { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateTime BillDate { get; set; } = DateTime.Today;
        public string BillStatus { get; set; } = string.Empty;//Paid, Unpaid

    }

    public class Advances
    {
        public int AdvanceID { get; set; }
        public string ConsumerID { get; set; }
        public double Amount { get; set; } // Advance Amount
        public DateTime DateInserted { get; set; }
        public DateTime? DateUsed { get; set; }
        public string Status { get; set; } //Used or Unused
        public double totalAmount { get; set; }
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
}
