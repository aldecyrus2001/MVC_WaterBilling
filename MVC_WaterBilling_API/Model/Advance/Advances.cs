using Microsoft.EntityFrameworkCore;

namespace MVC_WaterBilling_API.Model.Advance
{
    [Index("AdvanceID", IsUnique = true)]
    public class Advances
    {
        public int AdvanceID { get; set; }
        public string ConsumerID { get; set; }
        public double Amount { get; set; } // Advance Amount
        public DateTime DateInserted { get; set; }
        public DateTime DateUsed { get; set; }
        public string Status { get; set; } //Used or Unused
    }
}
