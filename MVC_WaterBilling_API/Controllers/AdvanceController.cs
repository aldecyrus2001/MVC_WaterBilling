using Microsoft.AspNetCore.Mvc;
using MVC_WaterBilling_API.Data;
using MVC_WaterBilling_API.Model.Advance;

namespace MVC_WaterBilling_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvanceController : Controller
    {
        private readonly AdvanceData _advanceData;

        public AdvanceController(AdvanceData advancesData)
        {
            _advanceData = advancesData;
        }

        [HttpGet("{ConsumerID}")]
        public async Task<IActionResult> GetAdvance(string ConsumerID)
        {
            if (string.IsNullOrEmpty(ConsumerID))
            {
                return BadRequest(new { message = "ConsumerID is required." });
            }

            var advance = await _advanceData.GetAdvancesAsync(ConsumerID);

            if (advance == null || ((dynamic)advance).Advances.Count == 0) // Casting to dynamic to access Advances
            {
                return NotFound(new { message = $"No advances found for ConsumerID: {ConsumerID}" });
            }

            return Ok(advance);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAdvance([FromBody] AdvanceDTO advanceDTO)
        {
            var advance = new Advances
            {
                ConsumerID = advanceDTO.ConsumerID,
                Amount = advanceDTO.Amount,
                DateInserted = DateTime.Now,
                Status = "Unused",
            };

            await _advanceData.CreateAdvanceAsync(advance);

            return Ok(new
            {
                message = "Advance Inserted successfully!"
            });
        }

        [HttpPut]
        public async Task<IActionResult> EditAdvance([FromBody] List<AdvanceDTO> advanceDTOs)
        {
            if (advanceDTOs == null || !advanceDTOs.Any())
            {
                return BadRequest(new { message = "No advances provided for update." });
            }

            var advancesToUpdate = new List<Advances>();

            foreach (var advanceDTO in advanceDTOs)
            {
                var advance = await _advanceData.GetAdvanceAsync(advanceDTO.AdvanceID);

                if (advance == null)
                {
                    return NotFound(new { message = $"Advance with ID {advanceDTO.AdvanceID} not found." });
                }

                advance.Status = "Used";
                advance.DateUsed = DateTime.Now;

                advancesToUpdate.Add(advance);
            }

            // Bulk update all changes
            await _advanceData.UpdateMultipleAdvanceStatusAsync(advancesToUpdate);

            return Ok(new
            {
                message = "Advances updated successfully!"
            });
        }
    }
}
