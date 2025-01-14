namespace MVC_WaterBilling_API.Model.Penalty
{
    public class Penalties
    {
        public int PenaltiesID { get; set; }
        public string ConsumerID { get; set; }
        public double PenaltyAmount { get; set; }
        public DateTime DateImplemented { get; set; }
    }
}
