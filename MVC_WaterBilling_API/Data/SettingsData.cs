using MVC_WaterBilling_API.Model.Settings;
using MVC_WaterBilling_API.Services;

namespace MVC_WaterBilling_API.Data
{
    public class SettingsData
    {
        private readonly ApplicationDBContext _db;

        private SettingsData(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<Settings?> GetSettingsByIdAsync(int id)
        {
            return await _db.Settings.FindAsync(id);
        }

        public async Task UpdateSettingAsync(Settings settings)
        {
            _db.Settings.Update(settings);
            await _db.SaveChangesAsync();
        }
    }
}
