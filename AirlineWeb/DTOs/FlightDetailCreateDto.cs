using System.ComponentModel.DataAnnotations;
namespace AirlineWeb.DTOs
{
 public class FlightDetailCreateDto
    {
        [Required]
        public string FlightCode { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}