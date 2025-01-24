﻿namespace MVC_FrontEnd.Models
{
    public class UsersDTO
    {
        public string Firstname { get; set; }
        public string? Middlename { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } //Administrator, Consumer, Cashier, FieldReader
    }

    public class ConsumerDTO
    {
        public string Address { get; set; }
        public string ConnectionType { get; set; } //Residential, Commercial
        public string Connection_Date { get; set; } //XXXX-XXXX-XXXX-XXXX
        public string Meter_Number { get; set; }
        public string Consumer_Status { get; set; } //Disconnected, Connected
    }
}
