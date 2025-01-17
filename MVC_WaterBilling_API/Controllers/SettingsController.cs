using Microsoft.AspNetCore.Mvc;
using MVC_WaterBilling_API.Data;
using MVC_WaterBilling_API.Model.Settings;

namespace MVC_WaterBilling_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : Controller
    {

        private readonly SettingsData _settingsData;

        public SettingsController(SettingsData settingsData)
        {
            _settingsData = settingsData;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSettings(int id)
        {
            var settings = await _settingsData.GetSettingsByIdAsync(id);
            if (settings == null)
            {
                return NotFound();
            }

            return Ok(settings);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSettings(int id, [FromBody] SettingsDTO settingsDTO)
        {
            var settings = await _settingsData.GetSettingsByIdAsync(id);
            if (settings == null)
            {
                return NotFound();
            }

            settings.SystemName = settingsDTO.SystemName;
            settings.PenaltyAmount = settingsDTO.PenaltyAmount;
            settings.AmountPerCubic = settingsDTO.AmountPerCubic;

            await _settingsData.UpdateSettingAsync(settings);

            return Ok(new
            {
                message = "Settings Updated Successfully!"
            });
        }
    }
}
