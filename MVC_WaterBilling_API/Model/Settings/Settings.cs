using Microsoft.EntityFrameworkCore;

namespace MVC_WaterBilling_API.Model.Settings
{
    [Index("SettingID", IsUnique = true)]
    public class Settings
    {
        public int SettingID { get; set; }
        public string? SystemName { get; set; }
        public double? PenaltyAmount { get; set; } = 0;
        public double? AmountPerCubic { get; set; } = 0;
        public double? AmountPerCubicCommercial { get; set; } = 0;
        public byte[]? GcashQr { get; set; }
        public string? Gcash_Name { get; set; }
    }
}
