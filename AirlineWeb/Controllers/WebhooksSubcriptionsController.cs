using Microsoft.AspNetCore.Mvc;
using AirlineWeb.Data;
using AirlineWeb.DTOs;
using AirlineWeb.Models;
using AutoMapper;

namespace AirlineWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhooksSubcriptionsController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;


        public WebhooksSubcriptionsController(AirlineDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        [HttpGet("{secret}", Name = "GetSubscriptionBySecret")]
        public ActionResult<WebhookSubscriptionReadDto> GetSubscriptionBySecret(string secret)
        {
            var subscription = _context.WebhooksSubcriptions
                                 .FirstOrDefault(s => s.Secret == secret);
            if (subscription == null)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<WebhookSubscriptionReadDto>(subscription));
        }


        [HttpPost]
        public ActionResult<WebhookSubscriptionReadDto> CreateSubcrition(WebhookSubscriptionCreateDto message)
        {
            var subscription = _context.WebhooksSubcriptions
                                .FirstOrDefault(s => s.WebhookURI == message.WebhookURI);
            if (subscription == null)
            {
                subscription = _mapper.Map<WebhooksSubcriptions>(message);
                subscription.Secret = Guid.NewGuid().ToString();
                subscription.WebhookPublisher = "PanAus";
                try
                {
                    _context.WebhooksSubcriptions.Add(subscription);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                var webhookSubscriptionReadDto = _mapper.Map<WebhookSubscriptionReadDto>(subscription);
                return CreatedAtRoute(nameof(GetSubscriptionBySecret), new { secret = webhookSubscriptionReadDto.Secret },webhookSubscriptionReadDto);
            }
            else
            {
                return NoContent();
            }

        }
    }
}
