using System.Text.Json.Serialization;

namespace DentaClinic.Models.Dtos
{
    public class FreeVisitPostDto
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("time")]
        public string Time { get; set; }
        [JsonPropertyName("serviceId")]
        public int ServiceId { get; set; }
    }
}
