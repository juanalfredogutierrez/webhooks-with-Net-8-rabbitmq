using AutoMapper;
using AirlineWeb.DTOs;
using AirlineWeb.Models;

namespace WebhookSubcriptionProfile.WebhookSubcriptionProfile
{
    public class WebhookSubcriptionProfile : Profile
    {
        public WebhookSubcriptionProfile()
        {
            CreateMap<WebhookSubscriptionCreateDto,WebhooksSubcriptions>();
            CreateMap<WebhooksSubcriptions,WebhookSubscriptionReadDto>();
        }
    }
}