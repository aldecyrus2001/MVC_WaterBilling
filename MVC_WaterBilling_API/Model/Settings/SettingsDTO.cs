namespace MVC_WaterBilling_API.Model.Settings
{
    public class SettingsDTO
    {
        public string? SystemName { get; set; }
        public double PenaltyAmount { get; set; }
        public double AmountPerCubic { get; set; }
    }

    public class SettingsQrDTO
    {
        public byte[]? GcashQr { get; set; }
        public string? Gcash_Name { get; set; }
    }
}
