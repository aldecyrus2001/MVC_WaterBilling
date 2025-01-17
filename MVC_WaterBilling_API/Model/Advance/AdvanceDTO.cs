using System.ComponentModel.DataAnnotations;

namespace MVC_WaterBilling_API.Model.Advance
{
    public class AdvanceDTO
    {
        public int AdvanceID { get; set; }

        [Required(ErrorMessage = "Consumer ID is required!")]
        public string ConsumerID { get; set; }

        [Required(ErrorMessage = "Amount is required!")]
        public double Amount { get; set; } // Advance Amount

        

    }
}
