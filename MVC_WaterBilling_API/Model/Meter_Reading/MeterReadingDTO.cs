using System.ComponentModel.DataAnnotations;

namespace MVC_WaterBilling_API.Model.Meter_Reading
{
    public class MeterReadingDTO
    {
        [Required(ErrorMessage = "Meter Number is required!")]
        public string Meter_Number { get; set; }

        [Required(ErrorMessage = "Reader ID is required!")]
        public string ReaderID { get; set; } //From User Role (FieldReader)

        [Required(ErrorMessage = "Previous Reading is required!")]
        public double Previous_Reading { get; set; }

        [Required(ErrorMessage = "Current Reading is required!")]
        public double Current_Reading { get; set; }

        [Required(ErrorMessage = "Usage is required!")]
        public double Usage { get; set; }

        [Required(ErrorMessage = "Status is required!")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Reading Date is required!")]
        public DateTime Reading_Date { get; set; }
    }
}
