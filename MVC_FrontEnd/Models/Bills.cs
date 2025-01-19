namespace MVC_FrontEnd.Models
{
    public class Bills
    {
        // Bills Model
        public int BillID { get; set; }
        public string ReadingID { get; set; }
        public double Consumed_Amount { get; set; } //The total amount consumed by the consumers
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime BillDate { get; set; }
        public string BillStatus { get; set; } //Paid, Unpaid

        //Meter model
        public string Meter_Number { get; set; }
        public string ReaderID { get; set; } //From User Role (FieldReader)
        public double Previous_Reading { get; set; }
        public double Current_Reading { get; set; }
        public double Usage { get; set; }
        public string Status { get; set; }
        public DateTime Reading_Date { get; set; }

        // Consumer model
        public int ConsumerID { get; set; }
        public string UserID { get; set; }
        public string Address { get; set; }
        public string ConnectionType { get; set; } //Residential, Commercial
        public DateTime Connection_Date { get; set; } //XXXX-XXXX-XXXX-XXXX
        public string Consumer_Status { get; set; } //Disconnected, Connected

        // User Model
        public string Firstname { get; set; }
        public string? Middlename { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Date_Created { get; set; }
        public string User_Status { get; set; } //Deleted, Active
        public string Role { get; set; } //Administrator, Consumer, Cashier, FieldReader
        public string? Token { get; set; } //Used to access the website as Authorization
    }
}
