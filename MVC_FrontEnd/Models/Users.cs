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
}
