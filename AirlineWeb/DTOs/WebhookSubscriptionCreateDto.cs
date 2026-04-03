using System.ComponentModel.DataAnnotations;
namespace AirlineWeb.DTOs
{
    public class WebhookSubscriptionCreateDto
    {
        [Required]
        public string WebhookURI { get; set; }

        [Required]
        public string WebhookType { get; set; }
    }
}