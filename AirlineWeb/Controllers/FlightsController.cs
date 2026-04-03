using Microsoft.AspNetCore.Mvc;
using AirlineWeb.Data;
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

        public FlightsControllercs(AirlineDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            _mapper.Map(flightDetailUpdateDto, flight);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
