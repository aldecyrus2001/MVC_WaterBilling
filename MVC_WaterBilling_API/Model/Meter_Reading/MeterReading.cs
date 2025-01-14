namespace MVC_WaterBilling_API.Model.Meter_Reading
{
    public class MeterReading
    {
        public int ReadingID { get; set; }
        public string Meter_Number { get; set; }
        public string ReaderID { get; set; } //From User Role (FieldReader)
        public double Previous_Reading { get; set; }
        public double Current_Reading { get; set; }
        public double Usage {  get; set; }
        public string Status { get; set; } 
    }
}
