using MVC_WaterBilling_API.Model.Advance;
using MVC_WaterBilling_API.Model.Consumer;
using MVC_WaterBilling_API.Model.User;
using System.ComponentModel.DataAnnotations;

namespace MVC_WaterBilling_API.Model.Meter_Reading
{
    public class MeterReadingDTO
    {
        [Required(ErrorMessage = "Meter Number is required! Meter")]
        public string meter_Number { get; set; }

        [Required(ErrorMessage = "Reader ID is required! Meter")]
        public string readerID { get; set; } //From User Role (FieldReader)
        public double previous_Reading { get; set; }

        [Required(ErrorMessage = "Current Reading is required!")]
        public double current_Reading { get; set; }

        [Required(ErrorMessage = "Usage is required!")]
        public double usage { get; set; }

        [Required(ErrorMessage = "Month is required!")]
        public string MonthOf { get; set; }

        [Required(ErrorMessage = "Reading Date is required!")]
        public DateTime reading_Date { get; set; } = DateTime.Now;
    }

    public class ReadingConsumerUserDTO
    {
        public Users Users { get; set; }
        public Consumers Consumers { get; set; }
        public MeterReading MeterReading { get; set; }
    }
}
