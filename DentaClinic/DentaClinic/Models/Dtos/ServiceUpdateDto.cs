using System.Text.Json.Serialization;

namespace DentaClinic.Models.Dtos
{
    public class ServiceUpdateDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
