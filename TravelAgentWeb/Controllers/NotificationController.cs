using Microsoft.AspNetCore.Mvc;
using TravelAgentWeb.Data;
using TravelAgentWeb.Dtos;
using TravelAgentWeb.Models;
using AutoMapper;

namespace TravelAgentWeb.Controllers
{
    [Route("api/Notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly TravelAgentDbContext _context;
        private readonly IMapper _mapper;

        public NotificationsController(TravelAgentDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult FlightChanged(FlightDetailUpdateDto flightDetailUpdateDto)
        {
            Console.WriteLine($"webhook Recevied from: {flightDetailUpdateDto.Publisher}");

            var secretModel = _context.WebhookSecrets
                                .FirstOrDefault(x => x.Publisher == flightDetailUpdateDto.Publisher &&
                                                x.Secret == flightDetailUpdateDto.Secret);
            if (secretModel == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Secret - Ignore webhook");
                Console.ResetColor();
                return Ok();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Valid webhook!");
                Console.WriteLine($"OldPrice {flightDetailUpdateDto.OldPrice}, New Price {flightDetailUpdateDto.NewPrice}");
                Console.ResetColor();
                return Ok();
            }


        }
    }
}