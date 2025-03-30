using Microsoft.AspNetCore.Mvc;
using MVC_WaterBilling_API.Data;
using MVC_WaterBilling_API.Model.Consumer;
using MVC_WaterBilling_API.Model.Meter_Reading;
using System.Data;
using System.Net;

namespace MVC_WaterBilling_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : Controller
    {
        private readonly ConsumerData _consumerData;
        private readonly UserData _userData;
        public ConsumerController(ConsumerData consumerData, UserData userData)
        {
            _consumerData = consumerData;
            _userData = userData;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumers()
        {
            var consumers = await _consumerData.GetConsumersWithUsersAsync();
            return Ok(consumers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsumer(int id)
        {
            var consumer = await _consumerData.GetConsumerWithUserAsync(id);
            if (consumer == null)
            {
                return NotFound();
            }

            return Ok(consumer); // Returns both Consumer and User in the response
        }

        [HttpGet("{meterNumber}/meterNumber")]
        public async Task<IActionResult> GetConsumerByMeterNumber(string meterNumber)
        {
            var consumer = await _consumerData.GetConsumerByMeterNumberAsync(meterNumber);
            if (consumer == null)
            {
                return NotFound();
            }

            return Ok(consumer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConsumer([FromBody] ConsumersDTO consumersDTO)
        {
            if (await _consumerData.isUserIdUsedAsync(consumersDTO.UserID))
            {
                return BadRequest(new
                {
                    message = "User Has already been already a member!"
                });
            }
            else if (await _consumerData.isMeterNumberUsedAsync(consumersDTO.Meter_Number))
            {
                return BadRequest(new
                {
                    message = "Meter Number Has already been already taken!"
                });
            }
            else
            {
                var consumer = new Consumers
                {
                    UserID = consumersDTO.UserID,
                    Address = consumersDTO.Address,
                    ConnectionType = consumersDTO.ConnectionType,
                    Connection_Date = consumersDTO.Connection_Date,
                    Meter_Number = consumersDTO.Meter_Number,
                    Consumer_Status = "Connected"
                };

                var meterReading = new MeterReading
                {
                    Meter_Number = consumersDTO.Meter_Number,
                    ReaderID = "",
                    Previous_Reading = 0,
                    Current_Reading = 0,
                    Usage = 0,
                    Status = "Billed",
                    Reading_Date = DateTime.UtcNow
                };

                var user = await _userData.GetUserByIdAsync(int.Parse(consumer.UserID));
                if(user != null)
                {
                    user.Applied = "Consumer";
                }

                await _consumerData.CreateConsumerAsync(consumer, meterReading);
                await _userData.UpdateUserAsync(user);

                return Ok(new
                {
                    message = "Consumer applied successfully!"
                });
            }

            

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditConsumer(int id, [FromBody] ConsumersDTO consumersDTO)
        {
            var consumer = await _consumerData.GetConsumerByIdAsync(id);
            if (consumer == null)
            {
                return NotFound();
            }

            if(await _consumerData.isMeterNumberUsedAsync(consumersDTO.Meter_Number) && consumer.Meter_Number != consumersDTO.Meter_Number)
            {
                return BadRequest(new
                {
                    message = "Meter number already used!"
                });
            }

            consumer.Address = consumersDTO.Address;
            consumer.ConnectionType = consumersDTO.ConnectionType;
            consumer.Connection_Date = consumersDTO.Connection_Date;
            consumer.Meter_Number = consumersDTO.Meter_Number;

            return Ok(new
            {
                message = "Consumer updated successfully!"
            });
        }

        [HttpPut("{id}/disconnect")]
        public async Task<IActionResult> DisconnectConsumer(int id)
        {
            var consumers = await _consumerData.GetConsumerByIdAsync(id);
            if (consumers == null)
            {
                return NotFound();
            }

            consumers.Consumer_Status = "Disconnected";

            await _consumerData.DisconnectConsumerAsync(consumers);

            return Ok(new
            {
                message = "Consumer disconnected successfully!"
            });
        }


        [HttpGet("{meterNumber}/searchConsumer")]
        public async Task<IActionResult> SearchConsumer(string meterNumber)
        {
            var consumerWithUser = await _consumerData.GetConsumerWithUser(meterNumber);
            if (consumerWithUser == null)
            {
                return NotFound(new { message = "Consumer not found for the given meter number." });
            }

            return Ok(consumerWithUser);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchCosumer([FromQuery] string? search = null)
        {
            var users = await _consumerData.SearchConsumerAsync(search);
            return Ok(users);
        }

    }
}
