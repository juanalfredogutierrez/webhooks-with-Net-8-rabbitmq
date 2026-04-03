using AutoMapper;
using AirlineWeb.DTOs;
using AirlineWeb.Models;

namespace WebhookSubcriptionProfile.WebhookSubcriptionProfile
{
    public class FlightDetailProfile : Profile
    {
        public FlightDetailProfile()
        {
            CreateMap<FlightDetailCreateDto, FlightDetail>();
            CreateMap<FlightDetail, FlightDetailReadDto>();


        }
    }
}