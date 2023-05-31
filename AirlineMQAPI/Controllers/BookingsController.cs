using AirlineMQAPI.Models;
using AirlineMQAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;

namespace AirlineMQAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {

        private readonly ILogger<BookingsController> _logger;
        private readonly IMessageProducer _messageProducer;

        //In-Memory db
        public static readonly List<Booking> _bookings = new();

        public BookingsController(ILogger<BookingsController> logger,IMessageProducer messageProducer)
        {
            _logger= logger;
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult CreatingBooking(Booking booking)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();

                
            }
            _bookings.Add(booking);
            _messageProducer.SendingMessage<Booking>(booking);

            return Ok();
            

        }


    }
}
