using MVC_WaterBilling_API.Model.User;

namespace MVC_WaterBilling_API.Model.Consumer
{
    public class Consumers
    {
        public int ConsumerID { get; set; }
        public string UserID { get; set; }
        public string Address { get; set; }
        public string ConnectionType { get; set; } //Residential, Commercial
        public DateTime Connection_Date { get; set; } //XXXX-XXXX-XXXX-XXXX
        public string Meter_Number { get; set; }
        public string Consumer_Status { get; set; } //Disconnected, Connected


    }

}
