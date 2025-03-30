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

        [HttpGet]
        public async Task<IActionResult> GetSettings()
        {
            var settings = await _settingsData.GetSettingsByIdAsync();
            if (settings == null)
            {
                return NotFound();
            }

            return Ok(settings);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSettings([FromBody] SettingsDTO settingsDTO)
        {
            var settings = await _settingsData.GetSettingsByIdAsync();
            if (settings == null)
            {
                return NotFound();
            }

            settings.SystemName = settingsDTO.SystemName;
            settings.AmountPerCubic = settingsDTO.AmountPerCubic;

            await _settingsData.UpdateSettingAsync(settings);

            return Ok(new
            {
                message = "Settings Updated Successfully!"
            });
        }

        [HttpPut("UpdateQr")]
        public async Task<IActionResult> UpdateQr([FromForm] IFormFile gcashQr, [FromForm] string gcashName)
        {
            var settings = await _settingsData.GetSettingsByIdAsync();
            if (settings == null)
            {
                return NotFound();
            }

            if (gcashQr != null)
            {
                using (var ms = new MemoryStream())
                {
                    await gcashQr.CopyToAsync(ms);
                    settings.GcashQr = ms.ToArray();
                }
            }

            settings.Gcash_Name = gcashName;
            await _settingsData.UpdateSettingAsync(settings);

            return Ok(new { message = "Settings Updated Successfully!" });
        }
    }
}
