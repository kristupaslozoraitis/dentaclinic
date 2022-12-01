using System.Text.Json.Serialization;

namespace DentaClinic.Models.Dtos
{
    public class VisitDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("time")]
        public string Time { get; set; }
        [JsonPropertyName("doctorFullName")]
        public string DoctorFullName { get; set; }
        [JsonPropertyName("service")]
        public string Service { get; set; }
    }
}
