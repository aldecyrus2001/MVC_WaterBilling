using MVC_WaterBilling_API.Model.User;
using System.ComponentModel.DataAnnotations;

namespace MVC_WaterBilling_API.Model.Consumer
{
    public class ConsumersDTO
    {
        [Required(ErrorMessage = "User ID is required!")]
        public string UserID { get; set; }
        
        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Connection type is required!")]
        public string ConnectionType { get; set; } //Residential, Commercial

        [Required(ErrorMessage = "Connection Date is required!")]
        public DateTime Connection_Date { get; set; } //XXXX-XXXX-XXXX-XXXX

        [Required(ErrorMessage = "Meter number is required!")]
        public string Meter_Number { get; set; }

    }

    public class ConsumerWithUser
    {
        public Consumers Consumer { get; set; }
        public Users User { get; set; }
    }

}
