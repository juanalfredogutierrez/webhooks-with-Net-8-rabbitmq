using System.ComponentModel.DataAnnotations;
namespace AirlineWeb.DTOs
{
 public class FlightDetailUpdateDto
    {
        [Required]
        public string FlightCode { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}