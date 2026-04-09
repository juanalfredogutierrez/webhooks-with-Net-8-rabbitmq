using Microsoft.AspNetCore.Mvc;
using AirlineWeb.Data;
using AirlineWeb.MessageBus;
using AirlineWeb.DTOs;
using AirlineWeb.Models;
using AutoMapper;

namespace AirlineWeb.Controllers
{
    [ApiController]
    [Route("api/Flights")]
    public class FlightsControllercs : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBus;
        public FlightsControllercs(AirlineDbContext context, IMapper mapper, IMessageBusClient messageBus)
        {
            _context = context;
            _mapper = mapper;
            _messageBus = messageBus;

        }

        [HttpGet("{flightCode}", Name = "GetFlightDetailsByCode")]
        public ActionResult<FlightDetailReadDto> GetFlightDetailsByCode(string flightCode)
        {
            var flight = _context.FlightDetails
                                 .FirstOrDefault(s => s.FlightCode == flightCode);

            if (flight == null)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<FlightDetailReadDto>(flight));
        }

        [HttpPost]
        public ActionResult<WebhookSubscriptionReadDto> CreateSubcrition(FlightDetailCreateDto message)
        {
            var flight = _context.FlightDetails
                                .FirstOrDefault(s => s.FlightCode == message.FlightCode);
            if (flight == null)
            {
                var flightDetailsModel = _mapper.Map<FlightDetail>(message);
                try
                {
                    _context.FlightDetails.Add(flightDetailsModel);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                var flightDetailsReadDto = _mapper.Map<FlightDetailReadDto>(flightDetailsModel);
                return CreatedAtRoute(nameof(GetFlightDetailsByCode), new { flightCode = flightDetailsReadDto.FlightCode }, flightDetailsReadDto);
            }
            else
            {
                return NoContent();
            }

        }

        [HttpPut("{id}")]
        public ActionResult UpdateFlightDetails(int id, FlightDetailUpdateDto flightDetailUpdateDto)
        {
            var flight = _context.FlightDetails
                                 .FirstOrDefault(s => s.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            decimal oldPrice = flight.Price;

            _mapper.Map(flightDetailUpdateDto, flight);

            try
            {
                _context.SaveChanges();

                if (oldPrice != flight.Price)
                {
                    Console.WriteLine("Price changed - Place message on bus");
                    var message = new NotificationMessageDto()
                    {
                        WebhookType = "priceChange",
                        FlightCode = flight.FlightCode,
                        OldPrice = oldPrice,
                        NewPrice = flight.Price
                    };
                    _messageBus.SendMessage(message);
                }
                else
                {
                    Console.WriteLine("No price change");
                }
                return NoContent();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
