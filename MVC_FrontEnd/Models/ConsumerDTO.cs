
namespace MVC_FrontEnd.Models
{
    public class ConsumerDTO
    {
        public string UserID { get; set; }
        public string Address { get; set; }
        public string ConnectionType { get; set; } //Residential, Commercial
        public string Connection_Date { get; set; } //XXXX-XXXX-XXXX-XXXX
        public string Meter_Number { get; set; }
        public string Consumer_Status { get; set; } //Disconnected, Connected
    }

    public class ConsumerWithUserDTO
    {
        public Consumers Consumer { get; set; }
        public Users User { get; set; }
    }
}
