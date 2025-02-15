using Microsoft.AspNetCore.Mvc;
using MVC_WaterBilling_API.Data;
using MVC_WaterBilling_API.Model.Consumer;
using MVC_WaterBilling_API.Model.Meter_Reading;

namespace MVC_WaterBilling_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterController : Controller
    {
        private readonly MeterReadingData _meterReadingData;

        public MeterController(MeterReadingData meterReadingData)
        {
            _meterReadingData = meterReadingData;
        }

        [HttpGet]
        public async Task<IActionResult> GetMeterReadings()
        {
            var meters = await _meterReadingData.GetMeterReadingsAsync();
            return Ok(meters);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeterReading(int id)
        {
            var meterReading = await _meterReadingData.GetMeterReadingsAsync(id);
            if(meterReading == null)
            {
                return NotFound();
            }

            return Ok(meterReading);
        }

        [HttpGet("{meternumber}/Search")]
        public async Task<IActionResult> GetMeterReadingByMeterNumber(string meternumber)
        {
            var meterReading = await _meterReadingData.GetMeterReadingByMeterNumberAsync(meternumber);
            if (meterReading == null)
            {
                return NotFound();
            }

            return Ok(meterReading);
        }


        [HttpGet("{meterNumber}/Previous-Reading")]
        public async Task<IActionResult> GetPreviousReading(string meterNumber)
        {
            var prevReading = await _meterReadingData.GetPreviousReadingAsync(meterNumber);
            if (prevReading == null)
            {
                return NotFound();
            }

            return Ok(prevReading);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReading([FromBody] MeterReadingDTO meterReadingDTO)
        {
            var meterReading = new MeterReading
            {
                Meter_Number = meterReadingDTO.meter_Number,
                ReaderID = meterReadingDTO.readerID,
                Previous_Reading = meterReadingDTO.previous_Reading,
                Current_Reading = meterReadingDTO.current_Reading,
                Usage = meterReadingDTO.usage,
                Status = "Unpaid",
                Reading_Date = DateTime.Now
            };

            bool isInserted = await _meterReadingData.CreateMeterReadingAsync(meterReading);

            if (!isInserted)
            {
                return BadRequest(new
                {
                    message = "Invalid Action, Meter Number Has Been Issued Reciept!."
                });
            }
            else
            {
                return Ok(new
                {
                    message = "Reading successfully inserted!",
                    readingId = meterReading.ReadingID
                });
            }

            
        }

    }
}
