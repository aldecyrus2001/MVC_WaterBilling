using System.ComponentModel.DataAnnotations;

namespace MVC_WaterBilling_API.Model.User
{
    public class UsersDTO
    {
        [Required(ErrorMessage = "Firstname is required!")]
        public string Firstname { get; set; }

        public string? Middlename { get; set; }

        [Required(ErrorMessage = "Lastname is required!")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Gender is required!")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Phone Number is required!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required!")]
        public string Role { get; set; } //Administrator, Consumer, Cashier, FieldReader

        public string? Token { get; set; } //Used to access the website as Authorization
    }

    public class UserUpdateDTO
    {

        [Required(ErrorMessage = "Firstname is required!")]
        public string Firstname { get; set; }

        public string? Middlename { get; set; }

        [Required(ErrorMessage = "Lastname is required!")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Gender is required!")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Phone Number is required!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required!")]
        public string Role { get; set; } //Administrator, Consumer, Cashier, FieldReader
    }
}
