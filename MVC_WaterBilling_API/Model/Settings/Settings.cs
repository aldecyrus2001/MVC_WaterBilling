using Microsoft.EntityFrameworkCore;

namespace MVC_WaterBilling_API.Model.Settings
{
    public class Settings
    {
        public int SettingID { get; set; }
        public string? SystemName { get; set; }
        public double PenaltyAmount { get; set; }
        public double AmountPerCubic { get; set; }
    }
}
